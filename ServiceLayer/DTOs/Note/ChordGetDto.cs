using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class ChordGetDto
    {
        public int Id { get; set; }
        public int MeasureId { get; set; }
        public double Duration { get; set; }
        //Vị trí nốt nhạc
        //Measure: thứ tự khuôn nhạc
        //Position: thứ tự note trong khuôn nhạc
        public int Position { get; set; }
        public int SlurPosition { get; set; }
        public ICollection<ChordNoteGetDto> ChordNotes { get; set; }
    }
}
