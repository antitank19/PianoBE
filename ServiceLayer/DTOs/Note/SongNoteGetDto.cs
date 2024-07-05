using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SongNoteGetDto
    {
        public int Id { get; set; }
        public int SheetID { get; set; }
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public string NotePitch { get; set; }
        public int NoteOctave { get; set; }
        public string Duration { get; set; }
        //Vị trí nốt nhạc
        //Measure: thứ tự khuôn nhạc
        //Position: thứ tự note trong khuôn nhạc
        public int MeasureId { get; set; }
        public int Position { get; set; }
    }
}
