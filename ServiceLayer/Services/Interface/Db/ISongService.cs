using Microsoft.AspNetCore.Http;
using ServiceLayer.CustomException;
using ServiceLayer.DTOs;
using ServiceLayer.ModelViews.Songs;
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
        Task<SongResponseByArtistPage> getSongsByArtistAndPage(int pageNum, int pageSize, string keyword);
        Task<SongResponseByGenrePage> getSongsByGenreAndPage(int pageNum, int pageSize, int? id, string? keyword);
        List<SongResponse> FindSongsByNameAsync(string username);
        Task<int> CountSongs();
        public Task<SongGetDto> UpdateSong(SongUpdateDto input);
        public Task DeleteSong(int id);
        
    }
}
