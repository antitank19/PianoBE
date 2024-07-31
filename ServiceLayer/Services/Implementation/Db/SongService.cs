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
    public class SongService : ISongService
    {
        private readonly PianoContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public SongService(PianoContext context, IMapper mapper, IConfiguration config)
        {
            this.context = context;
            this.mapper = mapper;
            this.config = config;
        }

        public IQueryable<T> GetSongList<T>()
        {
            return context.Songs.ProjectTo<T>(mapper.ConfigurationProvider);
        }

        public async Task<T> GetSongById<T>(int id)
        {
            Song song = await context.Songs
                 .Include(s => s.Sheets).ThenInclude(s => s.Instrument)
                 .Include(s => s.Sheets).ThenInclude(s => s.RightMeasures).ThenInclude(m => m.Chords).ThenInclude(c => c.ChordNotes).ThenInclude(cn => cn.Note)
                 .Include(s => s.Sheets).ThenInclude(s => s.LeftMeasures).ThenInclude(m => m.Chords).ThenInclude(c => c.ChordNotes).ThenInclude(cn => cn.Note)
                 .AsSingleQuery()
                 .SingleOrDefaultAsync(s => s.Id == id);
            T dto = mapper.Map<T>(song);
            return dto;
        }

        public async Task<SongGetDto> CreateSong(SongCreateDto input)
        {
            Song newSong = mapper.Map<Song>(input);
            newSong.Sheets.FirstOrDefault().ToSymbol(context.Notes.ToList());
            await context.Songs.AddAsync(newSong);
            await context.SaveChangesAsync();
            SongGetDto dto = mapper.Map<SongGetDto>(newSong);
            return dto;
        }

        public async Task<SongGetDto> CreateSong(SongSymbolCreateDto input)
        {
            Song newSong = mapper.Map<Song>(input);
            await context.Songs.AddAsync(newSong);
            await context.SaveChangesAsync();
            SongGetDto dto = mapper.Map<SongGetDto>(newSong);
            return dto;
        }

        public async Task<SongGetDto> CreateSong(SongMidiCreateDto input)
        {
            string midiUrl = await FirebaseStorageUtil.UploadFileAsync(input.Sheet.SheetFile, "Midi", config["Firebase:StorageBucket"]);
            Song newSong = new Song
            {
                ArtistId = input.ArtistId,
                GenreId = input.GenreId,
                Composer = input.Composer,
                Title = input.Title,
                Sheets = new Sheet[]
                {
                    new Sheet
                    {
                        BottomSignature = input.Sheet.BottomSignature,
                        TopSignature = input.Sheet.TopSignature,
                        InstrumentId = input.Sheet.InstrumentId,
                        SheetFile = midiUrl,
                    }
                },
            };
            await context.Songs.AddAsync(newSong);
            await context.SaveChangesAsync();
            SongGetDto dto = mapper.Map<SongGetDto>(newSong);
            return dto;
        }

        public async Task<bool> IsExistAsync(int songId)
        {
           return await context.Songs.AnyAsync(s => s.Id == songId);
        }
    }
}
