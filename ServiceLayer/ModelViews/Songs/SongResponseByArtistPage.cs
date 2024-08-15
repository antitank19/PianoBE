using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ModelViews.Songs
{
    public class SongResponseByArtistPage
    {
        public List<SongResponseByArtist> songResponseByArtists {  get; set; }
        public int TotalPage { get; set; }
        public int PageNum { get; set; }
    }
}
