using DataLayer.EnumsAndConsts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class ChordNoteCreateDto
    {
        public int ChordId { get; set; }
        public int NoteId { get; set; }
        public int Chromatic { get; set; } = (int)ChromaticEnum.Natural;
    }
}
