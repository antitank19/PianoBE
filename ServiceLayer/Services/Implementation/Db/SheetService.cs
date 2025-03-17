using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.IRepository;
using ServiceLayer.CustomException;
using ServiceLayer.DTOs;
using ServiceLayer.DTOs.Tracking;
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
        private readonly IUnitOfWork _unitOfWork;


        public SheetService(PianoContext context, IMapper mapper, IConfiguration config, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.mapper = mapper;
            this.config = config;
            this._unitOfWork = unitOfWork; 
        }

        public IQueryable<T> GetSheetList<T>()
        {
            return context.Sheets.Where(x => x.IsActive == true && x.IsDeleted == false)
                //.Include(s => s.LeftHandSheet).ThenInclude(s => s.Measures).ThenInclude(s => s.Chords).ThenInclude(s => s.ChordNotes).ThenInclude(sn => sn.Note)
                .ProjectTo<T>(mapper.ConfigurationProvider);
        }

        public IQueryable<T> GetSheetListBySongId<T>(int songId)
        {
            IQueryable<T> dtos = context.Sheets.Where(x => x.IsActive == true && x.IsDeleted == false)
                //.Include(s => s.LeftHandSheet)
                //.ThenInclude(s => s.Measures).ThenInclude(s => s.Chords).ThenInclude(s => s.ChordNotes).ThenInclude(sn => sn.Note)
                //.Include(s => s.Song)
                .Where(s => s.SongId == songId)
                .ProjectTo<T>(mapper.ConfigurationProvider);
            return dtos;
        }

        public async Task<T> GetSheetByIdAsync<T>(int sheetId)
        {
            Sheet sheet = await context.Sheets.Where(x => x.IsActive == true && x.IsDeleted == false)
                //.Include(s => s.LeftHandSheet).ThenInclude(s => s.Measures).ThenInclude(s => s.Chords).ThenInclude(s => s.ChordNotes).ThenInclude(sn => sn.Note)
                .Include(s => s.Song)
                .Include(s => s.Instrument)
                //.Include(s => s.RightMeasures).ThenInclude(s => s.Chords).ThenInclude(s => s.ChordNotes).ThenInclude(sn => sn.Note)
                //.Include(s => s.LeftMeasures).ThenInclude(s => s.Chords).ThenInclude(s => s.ChordNotes).ThenInclude(sn => sn.Note)
                .SingleOrDefaultAsync(x => x.Id == sheetId);
            T dto = mapper.Map<T>(sheet);
            return dto;
        }

        public async Task<SheetGetDto> CreateSheetAsync(SheetCreateDto input)
        {
            Sheet sheet = mapper.Map<Sheet>(input);
            //sheet.ToSymbol(context.Notes.ToList());
            sheet.CreatedTime  = DateTime.Now;

            if(input.BackgroundMusic != null)
            {
                string backgroundUrl = await FirebaseStorageUtil.UploadFileAsync(input.BackgroundMusic, "BackgroundMusic", config["Firebase:StorageBucket"]);
                sheet.BackgroundMusicFile = backgroundUrl;
            }

            await context.Sheets.AddAsync(sheet);
            await context.SaveChangesAsync();
            var dto = mapper.Map<SheetGetDto>(sheet);
            return dto;
        }

        public async Task<SheetGetDto> CreateSheetAsync(SheetSymbolCreateDto input)
        {
            Sheet sheet = new Sheet(input.SongId, input.InstrumentId, input.Name, input.Level,
                input.TopSignature, input.BottomSignature, input.KeySignature, input.RightSymbol, input.LeftSymbol);
            sheet.CreatedTime = DateTime.Now;

            if (input.BackgroundMusic != null)
            {
                string backgroundUrl = await FirebaseStorageUtil.UploadFileAsync(input.BackgroundMusic, "BackgroundMusic", config["Firebase:StorageBucket"]);
                sheet.BackgroundMusicFile = backgroundUrl;
            }

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
                SongId = input.SongId,
                InstrumentId = input.InstrumentId,
                BottomSignature = input.BottomSignature,
                TopSignature = input.TopSignature,
                MidiFile = midiUrl,
                CreatedTime = DateTime.Now,

                Level = input.Level,
                Name = input.Name,
            };

            if (input.BackgroundMusic != null)
            {
                string backgroundUrl = await FirebaseStorageUtil.UploadFileAsync(input.BackgroundMusic, "BackgroundMusic", config["Firebase:StorageBucket"]);
                sheet.BackgroundMusicFile = backgroundUrl;
            }

            await context.Sheets.AddAsync(sheet);
            await context.SaveChangesAsync();
            var dto = mapper.Map<SheetGetDto>(sheet);
            return dto;
        }


        public async Task<SheetGetDto> CreateSheetAsync(SheetXmlCreateDto input)
        {
            string xmlUrl = await FirebaseStorageUtil.UploadFileAsync(input.XmlFile, "Xml", config["Firebase:StorageBucket"]);
            Sheet sheet = new Sheet
            {
                BottomSignature = input.BottomSignature,
                TopSignature = input.TopSignature,
                InstrumentId = input.InstrumentId,
                XmlFile = xmlUrl,
                SongId = input.SongId,
                CreatedTime = DateTime.Now,
                RightSymbol = input.RightSymbol,
                LeftSymbol = input.LeftSymbol,

                Level = input.Level,
                Name = input.Name,
            };

            if (input.BackgroundMusic != null)
            {
                string backgroundUrl = await FirebaseStorageUtil.UploadFileAsync(input.BackgroundMusic, "BackgroundMusic", config["Firebase:StorageBucket"]);
                sheet.BackgroundMusicFile = backgroundUrl;
            }

            //sheet.DecodeSymbolToMeasure();

            await context.Sheets.AddAsync(sheet);
            await context.SaveChangesAsync();
            var dto = mapper.Map<SheetGetDto>(sheet);
            return dto;
        }

        public async Task<SheetGetDto> CreateSheetAsync(SheetMidiAndSymbolCreateDto input)
        {
            Sheet sheet = new Sheet(input.SongId, input.InstrumentId, input.Name, input.Level,
                input.TopSignature, input.BottomSignature, input.KeySignature, input.RightSymbol, input.LeftSymbol);
            
            string midiUrl = await FirebaseStorageUtil.UploadFileAsync(input.SheetFile, "Midi", config["Firebase:StorageBucket"]);
            sheet.MidiFile = midiUrl;
            
            sheet.CreatedTime = DateTime.Now;

            if (input.BackgroundMusic != null)
            {
                string backgroundUrl = await FirebaseStorageUtil.UploadFileAsync(input.BackgroundMusic, "BackgroundMusic", config["Firebase:StorageBucket"]);
                sheet.BackgroundMusicFile = backgroundUrl;
            }

            await context.Sheets.AddAsync(sheet);
            await context.SaveChangesAsync();
            var dto = mapper.Map<SheetGetDto>(sheet);
            return dto;
        }

        public async Task<SheetGetDto> UpdateSheetAsync(SheetUpdateDto input)
        {
            Sheet sheet = await _unitOfWork.SheetRepository.Entities
                .SingleOrDefaultAsync(p => p.IsActive == true && p.IsDeleted == false && p.Id == input.Id);
            if (sheet == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,
                    //"Song not found"); 
                    ErrorMessages.NOT_FOUND.Replace("0", "Song"));
            }
            mapper.Map(input, sheet);

            if (!String.IsNullOrWhiteSpace(input.RightSymbol))
            {
                sheet.RightSymbol = input.RightSymbol;
                //sheet.DecodeSymbolToMeasure();
            }

            if (input.MidiFile != null)
            {
                string midiUrl = await FirebaseStorageUtil.UploadFileAsync(input.MidiFile, "Midi", config["Firebase:StorageBucket"]);
                sheet.MidiFile = midiUrl;
            }

            if (input.XmlFile != null)
            {
                string xmlUrl = await FirebaseStorageUtil.UploadFileAsync(input.MidiFile, "Xml", config["Firebase:StorageBucket"]);
                sheet.XmlFile = xmlUrl;
            }

            if (input.BackgroundMusic != null)
            {
                string backgroundUrl = await FirebaseStorageUtil.UploadFileAsync(input.BackgroundMusic, "BackgroundMusic", config["Firebase:StorageBucket"]);
                sheet.BackgroundMusicFile = backgroundUrl;
            }
            /*_context.Instruments.Update(instrument);
            await _context.SaveChangesAsync();*/
            await _unitOfWork.SheetRepository.UpdateAsync(sheet);
            await _unitOfWork.SaveAsync();
            var response = mapper.Map<SheetGetDto>(sheet);
            return response;
        }


        public async Task<bool> IsExistAsync(int sheetId)
        {
            return await context.Songs.AnyAsync(s => s.Id == sheetId);
        }

        public async Task DeleteSheet(int id)
        {
            var sheet = await _unitOfWork.SheetRepository.FindAsync(p => p.Id == id && p.IsActive == true && p.IsDeleted == false);
            if (sheet == null)
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,
                    "Sheet not found");
            sheet.IsDeleted = true;
            _unitOfWork.SheetRepository.UpdateAsync(sheet);
            await _unitOfWork.SaveAsync();
        }
    }
}
