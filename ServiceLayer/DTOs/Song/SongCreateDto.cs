using DataLayer.DbObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SongCreateDto
    {
        [Required(ErrorMessage = "Title is required!")] 
        public string Title { get; set; }
        [Required(ErrorMessage = "Title is required!")] 
        public string Composer { get; set; }
        public string Genre { get; set; }
        public int ArtistId { get; set; }
        public ICollection<SheetCreateDto> Sheets { get; set; }
    }
}
