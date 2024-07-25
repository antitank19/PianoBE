using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceLayer.DTOs;
using ServiceLayer.Services.Interface.Db;
using ServiceLayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation.Db
{
    public class SheetService : ISheetService
    {
        private readonly PianoContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration config;


        public SheetService(PianoContext context, IMapper mapper, IConfiguration config)
        {
            this.context = context;
            this.mapper = mapper;
            this.config = config;
        }

        public IQueryable<T> GetSheetList<T>()
        {
            return context.Sheets
                //.Include(s => s.LeftHandSheet).ThenInclude(s => s.Measures).ThenInclude(s => s.Chords).ThenInclude(s => s.ChordNotes).ThenInclude(sn => sn.Note)
                .ProjectTo<T>(mapper.ConfigurationProvider);
        }

        public IQueryable<T> GetSheetListBySongId<T>(int songId)
        {
            IQueryable<T> dtos = context.Sheets
                //.Include(s => s.LeftHandSheet)
                //.ThenInclude(s => s.Measures).ThenInclude(s => s.Chords).ThenInclude(s => s.ChordNotes).ThenInclude(sn => sn.Note)
                //.Include(s => s.Song)
                .Where(s => s.SongId == songId)
                .ProjectTo<T>(mapper.ConfigurationProvider);
            return dtos;
        }

        public async Task<T> GetSheetByIdAsync<T>(int sheetId)
        {
            Sheet sheet = await context.Sheets
                //.Include(s => s.LeftHandSheet).ThenInclude(s => s.Measures).ThenInclude(s => s.Chords).ThenInclude(s => s.ChordNotes).ThenInclude(sn => sn.Note)
                .Include(s => s.Song)
                .Include(s => s.Instrument)
                .Include(s => s.RightMeasures).ThenInclude(s => s.Chords).ThenInclude(s => s.ChordNotes).ThenInclude(sn => sn.Note)
                .Include(s => s.LeftMeasures).ThenInclude(s => s.Chords).ThenInclude(s => s.ChordNotes).ThenInclude(sn => sn.Note)
                .SingleOrDefaultAsync(x => x.Id == sheetId);
            T dto = mapper.Map<T>(sheet);
            return dto;
        }

        public async Task<SheetGetDto> CreateSheetAsync(SheetCreateDto input)
        {
            Sheet sheet = mapper.Map<Sheet>(input);
            sheet.ToSymbol(context.Notes.ToList());
            await context.Sheets.AddAsync(sheet);
            await context.SaveChangesAsync();
            var dto = mapper.Map<SheetGetDto>(sheet);
            return dto;
        }

        public async Task<SheetGetDto> CreateSheetAsync(SheetSymbolCreateDto input)
        {
            Sheet sheet = new Sheet(input.SongId, input.InstrumentId, input.TopSignature, input.BottomSignature, input.RightSymbol, input.LeftSymbol);
            await context.Sheets.AddAsync(sheet);
            await context.SaveChangesAsync();
            var dto = mapper.Map<SheetGetDto>(sheet);
            return dto;
        }

        public async Task<SheetGetDto> CreateSheetAsync(SheetMidiCreateDto input)
        {
            string midiUrl = await FirebaseStorageUtil.UploadFileAsync(input.SheetFile, "Midi", config["Firebase:StorageBucket"]);
            Sheet sheet = new Sheet
            {
                BottomSignature = input.BottomSignature,
                TopSignature = input.TopSignature,
                InstrumentId = input.InstrumentId,
                SheetFile = midiUrl,
                SongId = input.SongId,
            };
            await context.Sheets.AddAsync(sheet);
            await context.SaveChangesAsync();
            var dto = mapper.Map<SheetGetDto>(sheet);
            return dto;
        }

        public async Task<bool> IsExistAsync(int sheetId)
        {
            return await context.Songs.AnyAsync(s => s.Id == sheetId);
        }
    }
}
