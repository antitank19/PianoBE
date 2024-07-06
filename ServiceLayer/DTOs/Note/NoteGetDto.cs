using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class NoteGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pitch { get; set; }
        public int Octave { get; set; }
    }
}
