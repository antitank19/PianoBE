using DataLayer.DbObject;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepository;
using ServiceLayer.CustomException;
using ServiceLayer.ModelViews.DashBoard;
using ServiceLayer.Services.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation.Db
{
    public class PlayTrackingService : IPlayTrackingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayTrackingService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountPlayerPlaying(DateTime from, DateTime to)
        {
            List<PlayTracking> playTracksInTime = await _unitOfWork.PlayTrackingRepository.FindListAsync(pt => 
                pt.IsActive && !pt.IsDeleted 
                && pt.CreatedTime > from && pt.CreatedTime < to
            );
            var playerIds = playTracksInTime.Select( p => p.PlayerId ).Distinct();
            return playerIds.Count();
        }

        public async Task<List<PlaysInYearResponse>> CountPlaysByYear(int year)
        {
            IQueryable<PlayTracking> playTrackings = _unitOfWork.GetRepository<PlayTracking>().Entities;
            List<PlaysInYearResponse> responses = new List<PlaysInYearResponse>();
            for (int i = 1; i <= 12; i++)
            {
                int number = await playTrackings.AsNoTracking().CountAsync(p => p.CreatedTime.Year == year && p.CreatedTime.Month == i );
                responses.Add(new PlaysInYearResponse
                {
                    month = i,
                    NumberPlays = number
                });
            }
            return responses;
        }

        public async Task createTrackingAsync(User user, Sheet sheet, int point)
        {
            PlayTracking dto = new PlayTracking()
            {
                CreatedBy = user.Id + "",
                CreatedTime = DateTime.Now,
                SheetId = sheet.Id,
                Point = point
            };
            await _unitOfWork.PlayTrackingRepository.InsertAsync(dto);
            await _unitOfWork.SaveAsync();
        }

        public async Task<int> getMaxPointBySheetId(int id)
        {
            IQueryable<PlayTracking> track = await _unitOfWork.PlayTrackingRepository.GetAllQueryableAsync();
            PlayTracking trackMax = await track.AsNoTracking().OrderByDescending(t => t.Point).Take(1).FirstOrDefaultAsync();
            if(trackMax == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "Point"));
            }
            return trackMax.Point;
        }

        public async Task<List<TopSongResponse>> GetTopSongByDays(string dateStart, string dateEnd)
        {
            IQueryable<PlayTracking> trackings = _unitOfWork.GetRepository<PlayTracking>().Entities;
            DateTime start = DateTime.Parse(dateStart);
            DateTime end = DateTime.Parse(dateEnd);
            var playTracking = await trackings.AsNoTracking()
                    .Include(track => track.Sheet)
                        .ThenInclude(sheet => sheet.Song)
                            .ThenInclude(song => song.Artist)
                    .Where(p => p.CreatedTime >= start && p.CreatedTime <= end)
                    .GroupBy(p => new { p.SheetId, p.Sheet.Song.Title, p.Sheet.Song.Artist.Name })
                    .OrderByDescending(g => g.Count())
                    .Select(g => new
                    {
                        SheetId = g.Key,
                        Count = g.Count(),
                        ArtistName = g.Key.Name,
                        SongName = g.Key.Title
                    })
                    .Take(3).ToListAsync();
            var topSongs = playTracking
                        .Select((g, index) => new TopSongResponse
                            {
                                Top = index + 1,
                                NumberPlays = g.Count,
                                ArtistName = g.ArtistName,
                                SongName = g.SongName
                            })
                        .ToList();
            return topSongs;
        }
    }
}
