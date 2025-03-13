using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.DashBoard;
using ServiceLayer.ModelViews.DashBoard;
using ServiceLayer.Services.Interface;
using System.Drawing.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;

        public DashBoardController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpGet]
        public async Task<IActionResult> getAllInformation(int year, String dateStart, String dateEnd)
        {
            int numberArtist = await _serviceWrapper.UserService.CountArtists();
            int numberUser = await _serviceWrapper.UserService.CountUsers();
            int numberSongs = await _serviceWrapper.SongService.CountSongs();
            List<PlaysInYearResponse> playsInYearResponses = await _serviceWrapper.PlayTrackingService.CountPlaysByYear(year);
            List<TopSongResponse> topSongResponses = await _serviceWrapper.PlayTrackingService.GetTopSongByDays(dateStart, dateEnd);
            DashBoardResponse dashBoardResponse = new DashBoardResponse()
            {
                ArtistNumber = numberArtist,
                UserNumber = numberUser,
                NumberSong = numberSongs,
                PlaysInYear = playsInYearResponses,
                TopSong = topSongResponses
            };
            return Ok(dashBoardResponse);
        }
    }
}
