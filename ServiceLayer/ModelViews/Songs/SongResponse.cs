using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ModelViews.Songs
{
    public class SongResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Composer { get; set; }
        public List<string> Genres { get; set; }
        public string CreatedTime {  get; set; }
        public string LastUpdatedTime { get; set;}
        public string Image {  get; set; }
    }
}
