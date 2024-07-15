using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface.Db
{
    public interface ISongService
    {
        public IQueryable<T> GetSongList<T>();
        public Task<T> GetSongById<T>(int id);
        public Task<SongGetDto> CreateSong(SongCreateDto input);
        public Task<SongGetDto> CreateSong(SongSymbolCreateDto input);
        public Task<SongGetDto> CreateSong(SongMidiCreateDto input);
        public Task<bool> IsExistAsync(int songId);

    }
}
