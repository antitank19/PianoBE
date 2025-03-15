using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs.Song
{
    public class FavoriteSongDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Composer { get; set; }
        public List<string> Genres { get; set; }
        public string Image { get; set; }
        public string ArtistName { get; set; }
    }
}
