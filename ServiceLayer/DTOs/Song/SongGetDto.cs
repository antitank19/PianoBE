using DataLayer.DbObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.ModelViews.Songs;

namespace ServiceLayer.DTOs
{
    public class SongGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Composer { get; set; }
        public List<string> GenreNames { get; set; }
        public string Image { get; set; }
        public int ArtistId { get; set; }
        public GetArtistInSongResponse Artist { get; set; }
        public ICollection<SheetGetDto> Sheets { get; set; } = new List<SheetGetDto>();
    }
}
