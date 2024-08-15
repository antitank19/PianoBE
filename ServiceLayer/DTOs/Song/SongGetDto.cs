using DataLayer.DbObject;
using ServiceLayer.ModelViews.Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SongGetDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Composer { get; set; }
        public string GenreName { get; set; }
        public int ArtistId { get; set; }
        public GetArtistInSongResponse Artist { get; set; }
        public ICollection<SheetGetDto> Sheets { get; set; } = new List<SheetGetDto>();
    }
}
