using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class ChordNoteGetDto
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int ChordId { get; set; }
        public int ChordPosition{ get; set; }
        public string NoteName { get; set; }
        public string NotePitch { get; set; }
        public int Chromatic { get; set; }
        public int NoteOctave { get; set; }
    }
}
