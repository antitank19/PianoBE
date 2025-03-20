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
using RepositoryLayer.IRepository;
using ServiceLayer.ModelViews.Songs;
using ServiceLayer.ModelViews.Users;
using ServiceLayer.PaggingItems;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ServiceLayer.CustomException;
using ServiceLayer.ModelViews.Instruments;

namespace ServiceLayer.Services.Implementation.Db
{
    public class SongService : ISongService
    {
        private readonly PianoContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly IUnitOfWork _unitOfWork;

        public SongService(PianoContext context, IMapper mapper, IConfiguration config, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.mapper = mapper;
            this.config = config;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<T> GetSongList<T>()
        {
            return context.Songs.Where(p => p.IsActive == true && p.IsDeleted == false).ProjectTo<T>(mapper.ConfigurationProvider);
        }

        public async Task<T> GetSongById<T>(int id)
        {
            Song song = await _unitOfWork.GetRepository<Song>().Entities.Where(x => x.IsActive == true && x.IsDeleted == false)
                .Include(p => p.Artist)
                .Include(p=>p.Genres)
                .Include(s => s.Sheets).ThenInclude(s => s.Instrument)
                //.Include(s => s.Sheets).ThenInclude(s => s.RightMeasures).ThenInclude(m => m.Chords).ThenInclude(c => c.ChordNotes).ThenInclude(cn => cn.Note)
                //.Include(s => s.Sheets).ThenInclude(s => s.LeftMeasures).ThenInclude(m => m.Chords).ThenInclude(c => c.ChordNotes).ThenInclude(cn => cn.Note)
                .AsSingleQuery()
                .SingleOrDefaultAsync(s => s.Id == id);
            T dto = mapper.Map<T>(song);
            return dto;
        }

        public async Task<SongGetDto> CreateSong(SongCreateDto input)
        {
            Genre? genre = await _unitOfWork.GenreRepository.FindAsync(genre => genre.Id == input.GenreId);
            if(genre == null)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ErrorMessages.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "Genre"));
            }
            Song newSong = mapper.Map<Song>(input);
            if (newSong.Sheets.Any())
            {
                //newSong.Sheets.FirstOrDefault().ToSymbol(context.Notes.ToList());
            }
            string imgUrl = await FirebaseStorageUtil.UploadFileAsync(input.ImageFile, "Image/Song", config["Firebase:StorageBucket"]);

            newSong.Image = imgUrl;
            newSong.CreatedTime = DateTime.Now;
            newSong.Genres = new Genre[] { genre };
            await _unitOfWork.SongRepository.InsertAsync(newSong);
            await _unitOfWork.SaveAsync();
            SongGetDto dto = mapper.Map<SongGetDto>(newSong);
            return dto;
        }

        public async Task<SongGetDto> CreateSong(SongSymbolCreateDto input)
        {
            Genre? genre = await _unitOfWork.GenreRepository.FindAsync(genre => genre.Id == input.GenreId);
            if (genre == null)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ErrorMessages.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "Genre"));
            }
            string midiUrl = await FirebaseStorageUtil.UploadFileAsync(input.Sheet.MidiFile, "Midi", config["Firebase:StorageBucket"]);
            Song newSong = mapper.Map<Song>(input);
            if (input.Sheet.BackgroundMusic != null)
            {
                string backgroundUrl = await FirebaseStorageUtil.UploadFileAsync(input.Sheet.BackgroundMusic, "BackgroundMusic", config["Firebase:StorageBucket"]);
                newSong.Sheets.FirstOrDefault().BackgroundMusicFile = backgroundUrl;
            }
            newSong.CreatedTime = DateTime.Now;
            newSong.Sheets.FirstOrDefault().MidiFile = midiUrl;
            await _unitOfWork.SongRepository.InsertAsync(newSong);
            await _unitOfWork.SongRepository.SaveAsync();
            SongGetDto dto = mapper.Map<SongGetDto>(newSong);
            return dto;
        }

        public async Task<SongGetDto> CreateSong(SongMidiCreateDto input)
        {
            string midiUrl = await FirebaseStorageUtil.UploadFileAsync(input.Sheet.SheetFile, "Midi", config["Firebase:StorageBucket"]);
            Genre genre = await _unitOfWork.GenreRepository.FindAsync(genre => genre.Id == input.GenreId);
            Song newSong = new Song
            {
                ArtistId = input.ArtistId,
                Composer = input.Composer,
                Title = input.Title,
                CreatedTime = DateTime.Now,
                Sheets = new Sheet[]
                {
                    new Sheet
                    {
                        BottomSignature = input.Sheet.BottomSignature,
                        TopSignature = input.Sheet.TopSignature,
                        InstrumentId = input.Sheet.InstrumentId,
                        MidiFile = midiUrl,
                    }
                },
                Genres = new Genre[]
                {
                    genre
                }
            };
            newSong.Genres = new Genre[] { genre };
            await _unitOfWork.SongRepository.InsertAsync(newSong);
            await _unitOfWork.SongRepository.SaveAsync();
            SongGetDto dto = mapper.Map<SongGetDto>(newSong);
            return dto;

        }

        public async Task<SongGetDto> UpdateSong(SongUpdateDto input)
        {

            //var song = await _unitOfWork.SongRepository.FindAsync(p => p.IsActive == true && p.IsDeleted == false && p.Id == input.Id);
            var song = await _unitOfWork.SongRepository.Entities.Include(x => x.Genres)
                .SingleOrDefaultAsync(p => p.IsActive == true && p.IsDeleted == false && p.Id == input.Id);
            if (song == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,
                    //"Song not found"); 
                    ErrorMessages.NOT_FOUND.Replace("0", "Song"));
            }
            var newSongGenre = await _unitOfWork.GenreRepository.FindAsync(p=>p.Id==input.GenreId);
            if (newSongGenre == null)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ErrorMessages.NOT_FOUND, 
                    ErrorMessages.NOT_FOUND.Replace("0", "Genre"));
            }
            mapper.Map(input, song);

            song.Genres = new List<Genre> { newSongGenre };
            //foreach (var oldGenre in song.Genres)
            //{
            //    song.Genres.Remove(oldGenre);
            //}
            //song.Genres.Add(newSongGenre);

            if (input.ImageFile != null)
            {
                string imgUrl = await FirebaseStorageUtil.UploadFileAsync(input.ImageFile, "Image/Song", config["Firebase:StorageBucket"]);
                song.Image = imgUrl; 
            }
            /*_context.Instruments.Update(instrument);
            await _context.SaveChangesAsync();*/
            await _unitOfWork.SongRepository.UpdateAsync(song);
            await _unitOfWork.SaveAsync();
            var response = mapper.Map<SongGetDto>(song);
            return response;

            //Song updated = new Song
            //{
            //    Id = input.Id,
            //    Title = input.Title,
            //    Image = input.Image,
            //    Composer = input.Composer,
            //    ArtistId=input.ArtistId,
            //    LastUpdatedTime = DateTime.Now,
            //};
            //await _unitOfWork.SongRepository.UpdateAsync(updated);
            //await _unitOfWork.SongRepository.SaveAsync();
            //SongGetDto dto = mapper.Map<SongGetDto>(updated);
            //return dto;
        }


        public async Task<bool> IsExistAsync(int songId)
        {
            var check = await _unitOfWork.SongRepository.FindAsync(s => s.Id == songId);
            if (check != null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        public async Task<SongResponseByArtistPage> getSongsByArtistAndPage(int pageNum, int pageSize, string keyword = "")
        {
            IQueryable<User> songQuery = _unitOfWork.GetRepository<User>().Entities
                .Where(user => user.UserRoles.Any(role => role.Role.Name == "Artist") 
                        && (string.IsNullOrWhiteSpace(keyword) || user.Name.ToLower().Contains(keyword.ToLower()))) // keyword = null (search all) 
                .Include(user => user.Songs
                .Where(song => song.IsActive == true && song.IsDeleted == false))
                    .ThenInclude(song => song.Genres)
                .OrderBy(user => user.Name);
            PaginatedList<User> paginatedList = await _unitOfWork.UserRepository.GetPagging(songQuery, pageNum, pageSize);
            List<SongResponseByArtist> pageDto = paginatedList.Items.Select(user => new SongResponseByArtist
            {
                Id = user.Id,
                Name = user.Name,
                DateOfBirth = user.DateOfBirth.ToString(),
                Songs = user.Songs
                .Where(song=>song.IsActive == true && song.IsDeleted == false)
                .Select(s => mapper.Map<SongResponse>(s)).ToList(),
            }).ToList();

            SongResponseByArtistPage page = new SongResponseByArtistPage
            {
                songResponseByArtists = pageDto,
                PageNum = paginatedList.PageNumber,
                TotalPage = paginatedList.TotalPages
            };
            return page;

        }

        public async Task<SongResponseByGenrePage> getSongsByGenreAndPage(int pageNum, int pageSize, int? id, string? keyword)
         {
            SongResponseByGenrePage page = null;
            IQueryable<Song> songQuery = _unitOfWork.GetRepository<Song>().Entities
                .Where(song=> song.IsActive == true && song.IsDeleted == false);
            if(id == null && keyword == null)
            {
                IQueryable<Song> query = songQuery.Include(s => s.Genres).Include(s => s.Artist);
                page = await SongResponseByQuery(query, pageNum, pageSize);
            }
            else if(id == null && keyword != null)
            {
                IQueryable<Song> query = songQuery.Include(s => s.Genres).Include(s => s.Artist)
                        .Where(song => song.Title.Contains(keyword));
                page = await SongResponseByQuery(query, pageNum, pageSize);
            }else if(id != null && keyword == null)
            {
                IQueryable<Song> query = songQuery.Include(s => s.Genres).Include(s => s.Artist)
                        .Where(song => song.Genres.Any(g => g.Id == id));
                page = await SongResponseByQuery(query, pageNum, pageSize);
            }
            else
            {
                IQueryable<Song> query = songQuery.Include(s => s.Genres).Include(s => s.Artist)
                        .Where(song => song.Genres.Any(g => g.Id == id))
                        .Where(song => song.Title.Contains(keyword));
                page = await SongResponseByQuery(query, pageNum, pageSize);
            }
            return page;
        }

        private async Task<SongResponseByGenrePage> SongResponseByQuery(IQueryable<Song> songQuery, int pageNum, int pageSize)
        {
            PaginatedList<Song> paginatedList = await _unitOfWork.GetRepository<Song>().GetPagging(songQuery, pageNum, pageSize);
            List<SongResponse> pageDto = paginatedList.Items.Select(s => mapper.Map<SongResponse>(s)).ToList();
            SongResponseByGenrePage page = new SongResponseByGenrePage
            {
                songResponseByGenre = pageDto,
                PageNum = paginatedList.PageNumber,
                TotalPage = paginatedList.TotalPages
            };
            return page;
        }

        public List<SongResponse> FindSongsByNameAsync(string username)
        {
            IQueryable<Song> songQuery = _unitOfWork.GetRepository<Song>().Entities
                .Where(song => song.Artist.UserName == username &&
                            song.IsActive == true && song.IsDeleted == false)
                .Include(song => song.Genres);
            List<SongResponse> listSong = songQuery.Select(s => mapper.Map<SongResponse>(s)).ToList();
            return listSong;
        }

        public async Task<int> CountSongs()
        {
            int number = await _unitOfWork.SongRepository.CountAsync();
            return number;
        }

        public async Task DeleteSong(int id)
        {
            /*
            var instrument = _context.Instruments.Where(x => x.IsActive == true && x.IsDeleted == false).FirstOrDefault(p => p.Id == id);
            */
            var song = await _unitOfWork.SongRepository.FindAsync(p => p.IsActive == true && p.IsDeleted == false && p.Id == id);
            if (song == null)
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND,
                    "Songs not found");
            song.IsDeleted = true;
            /*_context.Instruments.Update(instrument);
            _context.SaveChanges();*/
            await _unitOfWork.SongRepository.UpdateAsync(song);
            await _unitOfWork.SaveAsync();
        }
    }
}
