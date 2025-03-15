using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ModelViews.Songs
{
    public class SongResponseByGenrePage
    {
        public List<SongResponse> songResponseByGenre { get; set; }
        public int TotalPage { get; set; }
        public int PageNum { get; set; }
    }
}
