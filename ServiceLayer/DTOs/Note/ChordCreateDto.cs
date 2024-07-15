using DataLayer.DbObject;
using DataLayer.EnumsAndConsts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class ChordCreateDto
    {
        public double Duration { get; set; }
        // Flat:0, Thường:1, Sharp: 2
        //public int Chromatic { get; set; } = (int)ChromaticEnum.Natural;
        //Vị trí nốt nhạc
        //Measure: thứ tự khuôn nhạc
        //Position: thứ tự note trong khuôn nhạc
        public int MeasureId { get; set; }
        public int Position { get; set; }
        //public int[] NoteIds { get; set; }
        public ICollection<ChordNoteCreateDto> ChordNotes { get; set; }
    }
}
