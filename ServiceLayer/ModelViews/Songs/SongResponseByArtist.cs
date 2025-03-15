using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ModelViews.Songs
{
    public class SongResponseByArtist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String DateOfBirth { get; set; }
        public List<SongResponse> Songs { get; set; }
    }
}
