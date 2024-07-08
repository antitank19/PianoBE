using DataLayer.DbObject;
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
        public string Genre { get; set; }
        public int ArtistId { get; set; }
        public User Artist { get; set; }
        public int Signature1 { get; set; }
        public int Signature2 { get; set; }
        public ICollection<SheetGetDto> Sheets { get; set; } = new List<SheetGetDto>();
    }
}
