using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ModelViews.DashBoard
{
    public class TopSongResponse
    {
        public int Top {  get; set; }
        public int NumberPlays { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set;}

    }
}
