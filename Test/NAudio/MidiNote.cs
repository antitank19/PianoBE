using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.NAudio
{
    public class MidiNote
    {
        public int NoteNumber { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public double Duration => EndTime - StartTime;
        public int NoteLength { get; set; }
        public double DurationInBeat { get; set; }
        public double OffEventEndTime { get; set; }
        public int DeltaTime { get; set; }
        //public string Hand { get; set; }
    }
}
