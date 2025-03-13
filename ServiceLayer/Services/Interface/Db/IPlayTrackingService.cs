using DataLayer.DbObject;
using ServiceLayer.ModelViews.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface.Db
{
    public interface IPlayTrackingService
    {
        Task<List<PlaysInYearResponse>> CountPlaysByYear(int year);
        Task createTrackingAsync(User user, Sheet sheet, int point);
        Task<int> getMaxPointBySheetId(int id);
        Task<List<TopSongResponse>> GetTopSongByDays(string dateStart, string dateEnd);
    }
}
