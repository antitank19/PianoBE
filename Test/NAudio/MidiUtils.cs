public partial class MeasureValidator
{

    //public static void ReadMidiFileWithTimeSignature(string filePath)

    //string noteName = MidiUtils.GetNoteName(note.Item1);
    //string noteType = MidiUtils.GetNoteType(note.Item3, beatsPerMeasure);
    //double noteTypeDuration = MidiUtils.GetNoteTypeDuration(note.Item3, beatsPerMeasure);
    ////Console.WriteLine($"  {note.Item4} - Note: {noteName}, Start Time (beats): {note.Item2}, Duration (beats): {note.Item3}, Type: {noteType}");
    //if (note.Item4 == "Right Hand")
    //{
    //    Console.WriteLine($"  {note.Item4} - Note: {noteName}, Type: {noteType}, Type Duration: {noteTypeDuration}, Start Time (beats): {note.Item2}, Duration (beats): {note.Item3}");
    //}
    public static class MidiUtils
    {
        private static readonly string[] NoteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        //private static readonly string[] NoteNames = { "Do", "Do#", "Re", "re#", "Mi", "Fa", "Fa#", "Sol", "Sol#", "La", "La#", "Si" };
        private const double TimeThreshold = 0.1; // Threshold for synchronization in beats
        private const int PitchThreshold = 7; // Threshold for pitch difference in semitones


        public static string GetNoteName(int noteNumber)
        {
            int octave = (noteNumber / 12) - 1;
            int noteIndex = noteNumber % 12;
            return $"{NoteNames[noteIndex]}{octave}";
        }

        public static string GetNoteType(double durationBeats, int beatsPerMeasure)
        {
            if (durationBeats >= beatsPerMeasure)
            {
                return "tròn";
            }
            else if (durationBeats >= beatsPerMeasure / 2)
            {
                return "trắng";
            }
            else if (durationBeats >= beatsPerMeasure / 4)
            {
                return "đen";
            }
            else if (durationBeats >= beatsPerMeasure / 8)
            {
                return "móc đơn";
            }
            else if (durationBeats >= beatsPerMeasure / 16)
            {
                return "móc kép";
            }
            else
            {
                return "Unknown Note";
            }
        }
        public static double GetNoteTypeDuration(double durationBeats, int beatsPerMeasure)
        {
            if (durationBeats >= beatsPerMeasure)
            {
                return 4;
            }
            else if (durationBeats >= beatsPerMeasure / 2)
            {
                return 2;
            }
            else if (durationBeats >= beatsPerMeasure / 4)
            {
                return 1;
            }
            else if (durationBeats >= beatsPerMeasure / 8)
            {
                return 0.5;
            }
            else if (durationBeats >= beatsPerMeasure / 16)
            {
                return 0.25;
            }
            else
            {
                return -1;
            }
        }
        public static double RoundToThreshold(double value, double threshold)
        {
            return Math.Round(value / threshold) * threshold;
        }

        public static bool AreNotesInSameChord(double startTime1, double startTime2, int noteNumber1, int noteNumber2)
        {
            bool sameTime = Math.Abs(startTime1 - startTime2) <= TimeThreshold;
            bool sameHand = Math.Abs(noteNumber1 - noteNumber2) <= PitchThreshold;

            return sameTime && sameHand;
        }


        //
        public static int Compare(int noteNumber1, int noteNumber2)
        {
            bool sameHand = Math.Abs(noteNumber1 - noteNumber2) <= PitchThreshold;

            return -1;
        }

        public static bool AreNotesInSameTime(double startTime1, double startTime2)
        {
            return Math.Abs(startTime1 - startTime2) <= TimeThreshold;
        }

        public static string GetHand(int noteNumber, int middleC)
        {
            return noteNumber < middleC ? "Left Hand" : "Right Hand";
        }
    }
}
