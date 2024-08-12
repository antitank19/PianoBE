namespace Test.NAudio
{
    public class MidiChord
    {
        public double StartTime { get; set; }
        public List<MidiNote> Notes { get; set; } = new List<MidiNote>();
        public string Hand { get; set; }
    }
}
