using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ModelViews.DashBoard
{
    public class DashBoardResponse
    {
        public int? UserNumber { get; set; }
        public int? ArtistNumber {  get; set; }
        public int? NumberPlays { get; set; }
        public int? NumberSong { get; set; }
        public List<PlaysInYearResponse> PlaysInYear { get; set; }
        public List<TopSongResponse> TopSong { get; set; }

    }
}
