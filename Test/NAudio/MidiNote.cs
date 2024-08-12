using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.NAudio
{
    public class MidiNote
    {
        public double StartTime { get; set; }
        public int NoteNumber { get; set; }
        public int Length { get; set; }
        public int DeltaTime { get; set; }
        //public string Hand { get; set; }
    }
}
