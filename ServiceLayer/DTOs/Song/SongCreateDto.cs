using DataLayer.DbObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SongCreateDto
    {
        public string Title { get; set; }
        public string Composer { get; set; }
        public string Genre { get; set; }
        public int ArtistId { get; set; }
        public SheetCreateDto Sheet { get; set; }
    }
}
