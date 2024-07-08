using DataLayer.DbObject;
using ServiceLayer.DTOs.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class MeasureCreateDto
    {
        public int SheetId { get; set; }
        public int Position { get; set; }
        public ICollection<ChordNoteCreateDto> SongNotes { get; set; }
    }
}
