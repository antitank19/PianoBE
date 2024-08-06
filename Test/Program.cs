using NAudio.Midi;
using System;
using System.Collections.Generic;


public class MeasureValidator
{
    #region measure validator
    public class Note
    {
        public string Name { get; set; }
        public string Type { get; set; } // whole, half, quarter, eighth, sixteenth, etc.
    }
    public static double GetNoteDuration(string noteType)
    {
        switch (noteType)
        {
            case "whole": return 4.0;    // 1 whole note = 4 eighth notes
            case "half": return 2.0;     // 1 half note = 2 eighth notes
            case "quarter": return 1.0;  // 1 quarter note = 1 eighth note
            case "eighth": return 0.5;   // 1 eighth note = 0.5 eighth notes
            case "sixteenth": return 0.25; // 1 sixteenth note = 0.25 eighth notes
            default: throw new ArgumentException("Invalid note type");
        }
    }

    public static bool IsMeasureValid(List<Note> notes, int topSignature, int bottomSignature)
    {
        double totalNoteValues = 0;

        foreach (var note in notes)
        {
            totalNoteValues += GetNoteDuration(note.Type);
        }

        // Calculate the expected total note value in terms of eighth notes
        double expectedTotalValue = topSignature * (4.0 / bottomSignature); // For 5/8, it's simply 5 eighth notes

        // Check if the total note values match the expected value
        return Math.Abs(totalNoteValues - expectedTotalValue) < 0.0001; // Allow small floating-point error
    }

    private static void TestNumberOfBeat()
    {
        // Input: Time signature and list of notes
        int topSignature = 2; // 5/8 time signature
        int bottomSignature = 4;
        List<Note> notes = new List<Note>
        {
            new Note { Name = "C", Type = "quarter" }, // 1 eighth note (2 eighth notes)
            //new Note { Name = "D", Type = "eighth" },  // 1 eighth note (1 eighth note)
            new Note { Name = "E", Type = "sixteenth" }, // 0.5 eighth note
            new Note { Name = "F", Type = "sixteenth" }, // 0.5 eighth note
            //new Note { Name = "G", Type = "eighth" },  // 1 eighth note (1 eighth note)
            new Note { Name = "G", Type = "eighth" }  // 1 eighth note (1 eighth note)
        };

        // Validate the measure
        bool isValid = IsMeasureValid(notes, topSignature, bottomSignature);

        // Output the result
        Console.WriteLine($"Is the measure valid? {isValid}");
    }
    #endregion
    public static void Main()
    {
        //TestNumberOfBeat();
        //ReadMidiFile("D:\\FPT\\Piano\\f1d4cb7b-9e3b-445e-a3e7-f97fc78e5434_Sao_Sang.mid");
        ReadMidiFile("C:\\Users\\DELL\\Documents\\Zalo Received Files\\Sao_Sang.mid");
        
    }
    public static void ReadMidiFile(string filePath)
    {
        var midiFile = new MidiFile(filePath, false);

        // Default tempo and time signature
        double tempoBPM = 120.0;
        int beatsPerMeasure = 4;

        // Find the tempo and time signature of the MIDI file
        //foreach (var track in midiFile.Events)
        //{
        //    foreach (var midiEvent in track)
        //    {
        //        if (midiEvent is TempoEvent tempoEvent)
        //        {
        //            tempoBPM = tempoEvent.Tempo;
        //            Console.WriteLine("tempoBPM, " + tempoBPM);

        //        }
        //        else if (midiEvent is TimeSignatureEvent timeSignatureEvent)
        //        {
        //            beatsPerMeasure = timeSignatureEvent.Numerator;
        //            Console.WriteLine("beatsPerMeasure, "+ beatsPerMeasure);
        //        }
        //    }
        //}

        // Map to hold note on events and their start ticks
        var noteStartTicks = new Dictionary<int, long>();

        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is TempoEvent tempoEvent)
                {
                    tempoBPM = tempoEvent.Tempo;
                    Console.WriteLine("tempoBPM, " + tempoBPM);

                }
                else if (midiEvent is TimeSignatureEvent timeSignatureEvent)
                {
                    beatsPerMeasure = timeSignatureEvent.Numerator;
                    Console.WriteLine("beatsPerMeasure, " + beatsPerMeasure);
                }
                if (midiEvent is NoteOnEvent noteOnEvent && noteOnEvent.Velocity > 0)
                {
                    noteStartTicks[noteOnEvent.NoteNumber] = noteOnEvent.AbsoluteTime;
                }
                else if (midiEvent is NoteEvent noteEvent &&
                        (noteEvent is NoteOnEvent && noteEvent.Velocity == 0 ||
                         midiEvent.CommandCode == MidiCommandCode.NoteOff))
                {
                    if (noteStartTicks.ContainsKey(noteEvent.NoteNumber))
                    {
                        long startTick = noteStartTicks[noteEvent.NoteNumber];
                        long durationTicks = noteEvent.AbsoluteTime - startTick;
                        double ticksPerBeat = midiFile.DeltaTicksPerQuarterNote;
                        double durationBeats = (double)durationTicks / ticksPerBeat;

                        string noteName = MidiUtils.GetNoteName(noteEvent.NoteNumber);
                        string noteType = MidiUtils.GetNoteType(durationBeats, beatsPerMeasure);

                        Console.WriteLine($"Note: {noteName}, Duration: {durationBeats}, Type: {noteType}");
                        //Console.WriteLine($"Note: {noteName}, Type: {noteType}");
                        noteStartTicks.Remove(noteEvent.NoteNumber);
                    }
                }
            }
        }
    }
    //private static void ReadMidiFileOld(string filePath)
    //{
    //    var midiFile = new MidiFile(filePath, false);

    //    // Default tempo and time signature
    //    double tempoBPM = 120.0;
    //    int beatsPerMeasure = 4;

    //    // Find the tempo and time signature of the MIDI file
    //    foreach (var track in midiFile.Events)
    //    {
    //        foreach (var midiEvent in track)
    //        {
    //            if (midiEvent is TempoEvent tempoEvent)
    //            {
    //                tempoBPM = tempoEvent.Tempo;
    //            }
    //            else if (midiEvent is TimeSignatureEvent timeSignatureEvent)
    //            {
    //                beatsPerMeasure = timeSignatureEvent.Numerator;
    //            }
    //        }
    //    }

    //    // Map to hold note on events and their start ticks
    //    var noteStartTicks = new Dictionary<int, long>();

    //    foreach (var track in midiFile.Events)
    //    {
    //        foreach (var midiEvent in track)
    //        {
    //            if (midiEvent is NoteOnEvent noteOnEvent && noteOnEvent.Velocity > 0)
    //            {
    //                noteStartTicks[noteOnEvent.NoteNumber] = noteOnEvent.AbsoluteTime;
    //            }
    //            else if (midiEvent is NoteEvent noteEvent && (noteEvent is NoteOffEvent || noteOnEvent.Velocity == 0))
    //            {
    //                if (noteStartTicks.ContainsKey(noteEvent.NoteNumber))
    //                {
    //                    long startTick = noteStartTicks[noteEvent.NoteNumber];
    //                    long durationTicks = noteEvent.AbsoluteTime - startTick;
    //                    double ticksPerBeat = midiFile.DeltaTicksPerQuarterNote;
    //                    double durationBeats = (double)durationTicks / ticksPerBeat;

    //                    string noteName = MidiUtils.GetNoteName(noteEvent.NoteNumber);
    //                    string noteType = MidiUtils.GetNoteType(durationBeats, beatsPerMeasure);

    //                    Console.WriteLine($"Note: {noteName}, Duration (beats): {durationBeats}, Type: {noteType}");
    //                    noteStartTicks.Remove(noteEvent.NoteNumber);
    //                }
    //            }
    //        }
    //    }
    //}
    public static class MidiUtils
    {
        //private static readonly string[] NoteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        private static readonly string[] NoteNames = { "Do", "Do#", "Re", "re#", "Mi", "Fa", "Fa#", "Sol", "Sol#", "La", "La#", "Si" };

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
    }
}
