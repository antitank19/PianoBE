using DataLayer.DbObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SheetGetDto
    {
        public int Id { get; set; }

        public int SongId { get; set; }
        public string SongName { get; set; }
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public ICollection<SongNoteGetDto> SongNotes { get; set; }
    }
}
