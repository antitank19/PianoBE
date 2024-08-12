using NAudio.Midi;
using System;
using System.Collections.Generic;
using Test.NAudio;
using static MeasureValidator;


public partial class MeasureValidator
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
        //"C:\Users\DELL\Documents\Zalo Received Files\fur_elise.mid"
        //TestNumberOfBeat();
        //ReadMidiFile("D:\\FPT\\Piano\\f1d4cb7b-9e3b-445e-a3e7-f97fc78e5434_Sao_Sang.mid");
        //ReadMidiFileWithTimeSignature("C:\\Users\\DELL\\Documents\\Zalo Received Files\\Sao_Sang.mid");
        ReadMidiFileWithTimeSignatureOld("C:\\Users\\DELL\\Documents\\Zalo Received Files\\Sao_Sang.mid");
        Console.WriteLine("===========================================\n===========================================");
        Console.WriteLine("===========================================\n===========================================");
        ReadMidiFileWithChordAndSynchronize("C:\\Users\\DELL\\Documents\\Zalo Received Files\\Sao_Sang.mid");
        Console.WriteLine("===========================================\n===========================================");
        Console.WriteLine("===========================================\n===========================================");
        MyReadMidiFile("C:\\Users\\DELL\\Documents\\Zalo Received Files\\Sao_Sang.mid");
        //ReadMidiFileWithChordAndSynchronize("C:\\Users\\DELL\\Documents\\Zalo Received Files\\fur_elise.mid");
        //Console.WriteLine("===========================================\n===========================================");
        //MyReadMidiFile("C:\\Users\\DELL\\Documents\\Zalo Received Files\\fur_elise.mid");

    }
    #region NAuido
    private const int MiddleC = 60;
    #region old code

    public static void ReadMidiFileOld(string filePath)
    {
        var midiFile = new MidiFile(filePath, false);

        // Default tempo and time signature
        double tempoBPM = 120.0;
        int beatsPerMeasure = 4;

        // Find the tempo and time signature of the MIDI file
        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is TempoEvent tempoEvent)
                {
                    tempoBPM = tempoEvent.Tempo;
                }
                else if (midiEvent is TimeSignatureEvent timeSignatureEvent)
                {
                    beatsPerMeasure = timeSignatureEvent.Numerator;
                }
            }
        }

        // Calculate the ticks per beat based on tempo
        double ticksPerBeat = midiFile.DeltaTicksPerQuarterNote;

        // Map to hold note on events and their start ticks
        var noteStartTicks = new SortedDictionary<int, List<Tuple<long, double>>>();

        // Maps to hold note on events and their start ticks for left and right hands
        var leftHandNotes = new SortedDictionary<int, List<Tuple<long, double>>>();
        var rightHandNotes = new SortedDictionary<int, List<Tuple<long, double>>>();

        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is NoteOnEvent noteOnEvent && noteOnEvent.Velocity > 0)
                {
                    var noteDictionary = noteOnEvent.NoteNumber < MiddleC ? leftHandNotes : rightHandNotes;

                    if (!noteStartTicks.ContainsKey(noteOnEvent.NoteNumber))
                    {
                        noteStartTicks[noteOnEvent.NoteNumber] = new List<Tuple<long, double>>();
                    }
                    noteStartTicks[noteOnEvent.NoteNumber].Add(new Tuple<long, double>(noteOnEvent.AbsoluteTime, -1));
                }
                else if (midiEvent is NoteEvent noteEvent && (noteEvent is NoteOnEvent && noteEvent.Velocity == 0 || midiEvent.CommandCode == MidiCommandCode.NoteOff))
                {
                    if (noteStartTicks.ContainsKey(noteEvent.NoteNumber))
                    {
                        var startTick = noteStartTicks[noteEvent.NoteNumber].FirstOrDefault(tuple => tuple.Item2 == -1)?.Item1 ?? 0;
                        long durationTicks = noteEvent.AbsoluteTime - startTick;
                        double durationBeats = (double)durationTicks / ticksPerBeat;

                        noteStartTicks[noteEvent.NoteNumber].Remove(new Tuple<long, double>(startTick, -1));
                        noteStartTicks[noteEvent.NoteNumber].Add(new Tuple<long, double>(startTick, durationBeats));
                    }
                }
            }
        }

        // Organize notes into measures
        var notesInMeasures = new SortedDictionary<int, List<Tuple<int, double, double>>>();

        foreach (var note in noteStartTicks)
        {
            foreach (var timing in note.Value)
            {
                var measure = (int)Math.Floor(timing.Item1 / (ticksPerBeat * beatsPerMeasure));
                if (!notesInMeasures.ContainsKey(measure))
                {
                    notesInMeasures[measure] = new List<Tuple<int, double, double>>();
                }

                notesInMeasures[measure].Add(new Tuple<int, double, double>(note.Key, timing.Item1 / ticksPerBeat, timing.Item2));
            }
        }

        List<string> measureSymbols = new List<string>();
        // Print the notes organized by measure
        foreach (var measure in notesInMeasures)
        {
            Console.WriteLine($"Measure {measure.Key + 1}:");
            string measureSymbol = "";
            foreach (var note in measure.Value)
            {
                string noteName = MidiUtils.GetNoteName(note.Item1);
                string noteType = MidiUtils.GetNoteType(note.Item3, beatsPerMeasure);
                string noteTypeDuration = MidiUtils.GetNoteType(note.Item3, beatsPerMeasure);
                Console.WriteLine($"  Note: {noteName}, Type: {noteType}, Type Duration: {noteTypeDuration}, Start Time (beats): {note.Item2}, Duration (beats): {note.Item3}");
                measureSymbol += ($"{noteName}_{noteTypeDuration}");
            }
        }
    }
    public static void ReadMidiFile(string filePath)
    {
        var midiFile = new MidiFile(filePath, false);

        // Default tempo and time signature
        double tempoBPM = 120.0;
        int beatsPerMeasure = 4;

        // Find the tempo and time signature of the MIDI file
        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is TempoEvent tempoEvent)
                {
                    tempoBPM = tempoEvent.Tempo;
                }
                else if (midiEvent is TimeSignatureEvent timeSignatureEvent)
                {
                    beatsPerMeasure = timeSignatureEvent.Numerator;
                }
            }
        }

        // Calculate the ticks per beat based on tempo
        double ticksPerBeat = midiFile.DeltaTicksPerQuarterNote;

        // Maps to hold note on events and their start ticks for left and right hands
        var leftHandNotes = new SortedDictionary<int, List<Tuple<long, double>>>();
        var rightHandNotes = new SortedDictionary<int, List<Tuple<long, double>>>();

        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is NoteOnEvent noteOnEvent && noteOnEvent.Velocity > 0)
                {
                    var noteDictionary = noteOnEvent.NoteNumber < MiddleC ? leftHandNotes : rightHandNotes;

                    if (!noteDictionary.ContainsKey(noteOnEvent.NoteNumber))
                    {
                        noteDictionary[noteOnEvent.NoteNumber] = new List<Tuple<long, double>>();
                    }
                    noteDictionary[noteOnEvent.NoteNumber].Add(new Tuple<long, double>(noteOnEvent.AbsoluteTime, -1));
                }
                else if (midiEvent is NoteEvent noteEvent && (noteEvent is NoteOnEvent && noteEvent.Velocity == 0 || midiEvent.CommandCode == MidiCommandCode.NoteOff))
                {
                    var noteDictionary = noteEvent.NoteNumber < MiddleC ? leftHandNotes : rightHandNotes;

                    if (noteDictionary.ContainsKey(noteEvent.NoteNumber))
                    {
                        var startTick = noteDictionary[noteEvent.NoteNumber].FirstOrDefault(tuple => tuple.Item2 == -1)?.Item1 ?? 0;
                        long durationTicks = noteEvent.AbsoluteTime - startTick;
                        double durationBeats = (double)durationTicks / ticksPerBeat;

                        noteDictionary[noteEvent.NoteNumber].Remove(new Tuple<long, double>(startTick, -1));
                        noteDictionary[noteEvent.NoteNumber].Add(new Tuple<long, double>(startTick, durationBeats));
                    }
                }
            }
        }

        // Organize notes into measures
        var notesInMeasures = new SortedDictionary<int, List<Tuple<int, double, double, string>>>();

        void AddNotesToMeasures(SortedDictionary<int, List<Tuple<long, double>>> noteDictionary, string hand)
        {
            foreach (var note in noteDictionary)
            {
                foreach (Tuple<long, double> timing in note.Value)
                {
                    var measure = (int)Math.Floor(timing.Item1 / (ticksPerBeat * beatsPerMeasure));
                    if (!notesInMeasures.ContainsKey(measure))
                    {
                        notesInMeasures[measure] = new List<Tuple<int, double, double, string>>();
                    }

                    notesInMeasures[measure].Add(new Tuple<int, double, double, string>(note.Key, timing.Item1 / ticksPerBeat, timing.Item2, hand));
                }
            }
        }

        AddNotesToMeasures(leftHandNotes, "Left Hand");
        AddNotesToMeasures(rightHandNotes, "Right Hand");

        // Print the notes organized by measure
        foreach (var measure in notesInMeasures)
        {
            Console.WriteLine($"Measure {measure.Key + 1}:");
            foreach (var note in measure.Value)
            {
                string noteName = MidiUtils.GetNoteName(note.Item1);
                string noteType = MidiUtils.GetNoteType(note.Item3, beatsPerMeasure);
                double noteTypeDuration = MidiUtils.GetNoteTypeDuration(note.Item3, beatsPerMeasure);
                //Console.WriteLine($"  {note.Item4} - Note: {noteName}, Start Time (beats): {note.Item2}, Duration (beats): {note.Item3}, Type: {noteType}");
                if (note.Item4 == "Right Hand")
                {
                    Console.WriteLine($"  {note.Item4} - Note: {noteName}, Type: {noteType}, Type Duration: {noteTypeDuration}, Start Time (beats): {note.Item2}, Duration (beats): {note.Item3}");
                }
            }
        }
    }
    public static void ReadMidiFileWithTimeSignatureOld2(string filePath)
    {
        var midiFile = new MidiFile(filePath, false);

        // Default tempo and time signature
        double tempoBPM = 120.0;
        int beatsPerMeasure = 4;
        int bottomSignature = 4;

        // Find the tempo and time signature of the MIDI file
        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is TempoEvent tempoEvent)
                {
                    tempoBPM = tempoEvent.Tempo;
                }
                else if (midiEvent is TimeSignatureEvent timeSignatureEvent)
                {
                    beatsPerMeasure = timeSignatureEvent.Numerator;
                    bottomSignature = timeSignatureEvent.Denominator;
                }
            }
        }

        // Calculate the ticks per beat based on tempo
        double ticksPerBeat = midiFile.DeltaTicksPerQuarterNote;

        // Maps to hold note on events and their start ticks for left and right hands
        var leftHandNotes = new SortedDictionary<int, List<Tuple<long, double>>>();
        var rightHandNotes = new SortedDictionary<int, List<Tuple<long, double>>>();

        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is NoteOnEvent noteOnEvent && noteOnEvent.Velocity > 0)
                {
                    var noteDictionary = noteOnEvent.NoteNumber < MiddleC ? leftHandNotes : rightHandNotes;

                    if (!noteDictionary.ContainsKey(noteOnEvent.NoteNumber))
                    {
                        noteDictionary[noteOnEvent.NoteNumber] = new List<Tuple<long, double>>();
                    }
                    noteDictionary[noteOnEvent.NoteNumber].Add(new Tuple<long, double>(noteOnEvent.AbsoluteTime, -1));
                }
                else if (midiEvent is NoteEvent noteEvent && (noteEvent is NoteOnEvent && noteEvent.Velocity == 0 || midiEvent.CommandCode == MidiCommandCode.NoteOff))
                {
                    var noteDictionary = noteEvent.NoteNumber < MiddleC ? leftHandNotes : rightHandNotes;

                    if (noteDictionary.ContainsKey(noteEvent.NoteNumber))
                    {
                        var startTick = noteDictionary[noteEvent.NoteNumber].FirstOrDefault(tuple => tuple.Item2 == -1)?.Item1 ?? 0;
                        long durationTicks = noteEvent.AbsoluteTime - startTick;
                        double durationBeats = (double)durationTicks / ticksPerBeat;

                        noteDictionary[noteEvent.NoteNumber].Remove(new Tuple<long, double>(startTick, -1));
                        noteDictionary[noteEvent.NoteNumber].Add(new Tuple<long, double>(startTick, durationBeats));
                    }
                }
            }
        }

        // Organize notes into measures
        var notesInMeasures = new SortedDictionary<int, List<Tuple<int, double, double, string>>>();

        void AddNotesToMeasures(SortedDictionary<int, List<Tuple<long, double>>> noteDictionary, string hand)
        {
            foreach (var note in noteDictionary)
            {
                foreach (var timing in note.Value)
                {
                    var measure = (int)Math.Floor(timing.Item1 / (ticksPerBeat * beatsPerMeasure));
                    if (!notesInMeasures.ContainsKey(measure))
                    {
                        notesInMeasures[measure] = new List<Tuple<int, double, double, string>>();
                    }

                    notesInMeasures[measure].Add(new Tuple<int, double, double, string>(note.Key, timing.Item1 / ticksPerBeat, timing.Item2, hand));
                }
            }
        }

        AddNotesToMeasures(leftHandNotes, "Left Hand");
        AddNotesToMeasures(rightHandNotes, "Right Hand");

        // Print the notes organized by measure
        foreach (var measure in notesInMeasures)
        {
            Console.WriteLine($"Measure {measure.Key + 1} (Top Signature: {beatsPerMeasure}/{bottomSignature}):");
            foreach (var note in measure.Value)
            {
                string noteName = MidiUtils.GetNoteName(note.Item1);
                string noteType = MidiUtils.GetNoteType(note.Item3, beatsPerMeasure);
                double noteTypeDuration = MidiUtils.GetNoteTypeDuration(note.Item3, beatsPerMeasure);
                //Console.WriteLine($"  {note.Item4} - Note: {noteName}, Start Time (beats): {note.Item2}, Duration (beats): {note.Item3}, Type: {noteType}");
                if (note.Item4 == "Right Hand")
                {
                    Console.WriteLine($"  {note.Item4} - Note: {noteName}, Type: {noteType}, Type Duration: {noteTypeDuration}, Start Time (beats): {note.Item2}, Duration (beats): {note.Item3}");
                }
            }
        }
    }

    public static void ReadMidiFileWithTimeSignatureOld(string filePath)
    {
        var midiFile = new MidiFile(filePath, false);

        // Default tempo and time signature
        double tempoBPM = 120.0;
        int beatsPerMeasure = 4;
        int bottomSignature = 4;
        bool timeSignatureFound = false;

        // Find the tempo and time signature of the MIDI file
        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is TempoEvent tempoEvent)
                {
                    tempoBPM = tempoEvent.Tempo;
                }
                else if (midiEvent is TimeSignatureEvent timeSignatureEvent && !timeSignatureFound)
                {
                    beatsPerMeasure = timeSignatureEvent.Numerator;
                    bottomSignature = (int)Math.Pow(2, timeSignatureEvent.Denominator);
                    timeSignatureFound = true;
                    Console.WriteLine($"Initial time signature event: {beatsPerMeasure}/{bottomSignature}");
                }
            }
        }

        // Calculate the ticks per beat based on tempo
        double ticksPerBeat = midiFile.DeltaTicksPerQuarterNote;
        Console.WriteLine($"Ticks per beat: {ticksPerBeat}");

        // Maps to hold note on events and their start ticks for left and right hands
        var leftHandNotes = new SortedDictionary<int, List<Tuple<long, double>>>();
        var rightHandNotes = new SortedDictionary<int, List<Tuple<long, double>>>();

        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is NoteOnEvent noteOnEvent && noteOnEvent.Velocity > 0)
                {
                    var noteDictionary = noteOnEvent.NoteNumber < MiddleC ? leftHandNotes : rightHandNotes;

                    if (!noteDictionary.ContainsKey(noteOnEvent.NoteNumber))
                    {
                        noteDictionary[noteOnEvent.NoteNumber] = new List<Tuple<long, double>>();
                    }
                    noteDictionary[noteOnEvent.NoteNumber].Add(new Tuple<long, double>(noteOnEvent.AbsoluteTime, -1));
                }
                else if (midiEvent is NoteEvent noteEvent && (noteEvent is NoteOnEvent && noteEvent.Velocity == 0 || midiEvent.CommandCode == MidiCommandCode.NoteOff))
                {
                    var noteDictionary = noteEvent.NoteNumber < MiddleC ? leftHandNotes : rightHandNotes;

                    if (noteDictionary.ContainsKey(noteEvent.NoteNumber))
                    {
                        var startTick = noteDictionary[noteEvent.NoteNumber].FirstOrDefault(tuple => tuple.Item2 == -1)?.Item1 ?? 0;
                        long durationTicks = noteEvent.AbsoluteTime - startTick;
                        double durationBeats = (double)durationTicks / ticksPerBeat;

                        noteDictionary[noteEvent.NoteNumber].Remove(new Tuple<long, double>(startTick, -1));
                        noteDictionary[noteEvent.NoteNumber].Add(new Tuple<long, double>(startTick, durationBeats));
                    }
                }
            }
        }

        // Organize notes into measures
        var notesInMeasures = new SortedDictionary<int, List<Tuple<int, double, double, string>>>();

        void AddNotesToMeasures(SortedDictionary<int, List<Tuple<long, double>>> noteDictionary, string hand)
        {
            foreach (var note in noteDictionary)
            {
                foreach (var timing in note.Value)
                {
                    var measure = (int)Math.Floor(timing.Item1 / (ticksPerBeat * beatsPerMeasure));
                    if (!notesInMeasures.ContainsKey(measure))
                    {
                        notesInMeasures[measure] = new List<Tuple<int, double, double, string>>();
                    }

                    notesInMeasures[measure].Add(new Tuple<int, double, double, string>(note.Key, timing.Item1 / ticksPerBeat, timing.Item2, hand));
                }
            }
        }

        AddNotesToMeasures(leftHandNotes, "Left Hand");
        AddNotesToMeasures(rightHandNotes, "Right Hand");

        // Print the notes organized by measure
        foreach (var measure in notesInMeasures)
        {
            Console.WriteLine($"Measure {measure.Key + 1} (Time Signature: {beatsPerMeasure}/{bottomSignature}):");
            foreach (var note in measure.Value)
            {
                string noteName = MidiUtils.GetNoteName(note.Item1);
                string noteType = MidiUtils.GetNoteType(note.Item3, beatsPerMeasure);
                //Console.WriteLine($"  {note.Item4} - Note: {noteName}, Start Time (beats): {note.Item2}, Duration (beats): {note.Item3}, Type: {noteType}");
                double noteTypeDuration = MidiUtils.GetNoteTypeDuration(note.Item3, beatsPerMeasure);
                if (note.Item4 == "Right Hand")
                {
                    Console.WriteLine($"  {note.Item4} - Note: {noteName}, Type: {noteType}, Type Duration: {noteTypeDuration}, Start Time (beats): {note.Item2}, Duration (beats): {note.Item3}");
                }
            }
        }
    }


    public static void ReadMidiFileWithChordAndSynchronizeOld2(string filePath)
    {
        var midiFile = new MidiFile(filePath, false);

        // Calculate Middle C dynamically
        int middleC = 60; // MIDI note number for Middle C (C4)

        // Default tempo and time signature
        double tempoBPM = 120.0;
        int beatsPerMeasure = 4;
        int bottomSignature = 4;
        bool timeSignatureFound = false;

        // Find the tempo and time signature of the MIDI file
        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is TempoEvent tempoEvent)
                {
                    tempoBPM = tempoEvent.Tempo;
                }
                else if (midiEvent is TimeSignatureEvent timeSignatureEvent && !timeSignatureFound)
                {
                    beatsPerMeasure = timeSignatureEvent.Numerator;
                    bottomSignature = (int)Math.Pow(2, timeSignatureEvent.Denominator);
                    timeSignatureFound = true;
                    Console.WriteLine($"Initial time signature event: {beatsPerMeasure}/{bottomSignature}");
                }
            }
        }

        // Calculate the ticks per beat based on tempo
        double ticksPerBeat = midiFile.DeltaTicksPerQuarterNote;
        Console.WriteLine($"Ticks per beat: {ticksPerBeat}");

        // Maps to hold note on events and their start ticks
        var allNotes = new List<Tuple<double, int, string>>();
        var measures = new SortedDictionary<int, List<Tuple<double, int, string>>>();

        int currentMeasure = 0;
        double measureStartTime = 0;

        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is NoteOnEvent noteOnEvent && noteOnEvent.Velocity > 0)
                {
                    double startBeat = noteOnEvent.AbsoluteTime / ticksPerBeat;
                    var hand = MidiUtils.GetHand(noteOnEvent.NoteNumber, middleC);
                    allNotes.Add(new Tuple<double, int, string>(startBeat, noteOnEvent.NoteNumber, hand));
                }
                else if (midiEvent is NoteEvent noteEvent && (noteEvent is NoteOnEvent && noteEvent.Velocity == 0 || midiEvent.CommandCode == MidiCommandCode.NoteOff))
                {
                    var startNote = allNotes.FirstOrDefault(n => n.Item2 == noteEvent.NoteNumber);
                    if (startNote != null)
                    {
                        allNotes.Remove(startNote);
                        double startBeat = startNote.Item1;
                        double endBeat = noteEvent.AbsoluteTime / ticksPerBeat;
                        double durationBeats = endBeat - startBeat;
                        var hand = MidiUtils.GetHand(noteEvent.NoteNumber, middleC);
                        allNotes.Add(new Tuple<double, int, string>(startBeat, noteEvent.NoteNumber, hand));
                    }
                }
            }
        }

        // Group notes into measures
        foreach (var note in allNotes)
        {
            int measure = (int)(note.Item1 / beatsPerMeasure);
            if (!measures.ContainsKey(measure))
            {
                measures[measure] = new List<Tuple<double, int, string>>();
            }
            measures[measure].Add(note);
        }

        // Process each measure
        foreach (var measure in measures)
        {
            Console.WriteLine($"Measure {measure.Key + 1}:");
            var notesInMeasure = measure.Value;

            // Group notes into chords based on synchronization
            var chords = new List<Tuple<double, List<Tuple<int, string>>>>();

            foreach (var note in notesInMeasure)
            {
                var matchingChords = chords.Where(chord => MidiUtils.AreNotesInSameChord(chord.Item1, note.Item1, chord.Item2.First().Item1, note.Item2)).ToList();

                if (matchingChords.Any())
                {
                    var chord = matchingChords.First();
                    chord.Item2.Add(new Tuple<int, string>(note.Item2, note.Item3));
                }
                else
                {
                    chords.Add(new Tuple<double, List<Tuple<int, string>>>(note.Item1, new List<Tuple<int, string>> { new Tuple<int, string>(note.Item2, note.Item3) }));
                }
            }

            // Print the synchronized notes and chords for this measure
            foreach (var chord in chords)
            {
                var notesNames = chord.Item2
                    .Select(note => MidiUtils.GetNoteName(note.Item1))
                    .ToList();
                string notesName = string.Join(", ", notesNames);
                string noteType = MidiUtils.GetNoteType(chord.Item2.Count, beatsPerMeasure);

                if(chord.Item2.First().Item2== "Right Hand")
                {
                    Console.WriteLine($"  {chord.Item2.First().Item2} - Notes: {notesName}, Start Time (beats): {chord.Item1}, Duration (beats): {chord.Item2.Count}, Type: {noteType}");
                }
            }

        }
    }
    public static void ReadMidiFileWithChordAndSynchronizeOld(string filePath)
    {
        var midiFile = new MidiFile(filePath, false);

        // Calculate Middle C dynamically
        int middleC = 60; // MIDI note number for Middle C (C4)

        // Default tempo and time signature
        double tempoBPM = 120.0;
        int beatsPerMeasure = 4;
        int bottomSignature = 4;
        bool timeSignatureFound = false;

        // Find the tempo and time signature of the MIDI file
        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is TempoEvent tempoEvent)
                {
                    tempoBPM = tempoEvent.Tempo;
                }
                else if (midiEvent is TimeSignatureEvent timeSignatureEvent && !timeSignatureFound)
                {
                    beatsPerMeasure = timeSignatureEvent.Numerator;
                    bottomSignature = (int)Math.Pow(2, timeSignatureEvent.Denominator);
                    timeSignatureFound = true;
                    Console.WriteLine($"Initial time signature event: {beatsPerMeasure}/{bottomSignature}");
                }
            }
        }

        // Calculate the ticks per beat based on tempo
        double ticksPerBeat = midiFile.DeltaTicksPerQuarterNote;
        Console.WriteLine($"Ticks per beat: {ticksPerBeat}");

        // Maps to hold note on events and their start ticks
        var allNotes = new List<Tuple<double, int, string>>();
        var measures = new SortedDictionary<int, List<Tuple<double, int, string>>>();

        int currentMeasure = 0;
        double measureStartTime = 0;

        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is NoteOnEvent noteOnEvent && noteOnEvent.Velocity > 0)
                {
                    double startBeat = noteOnEvent.AbsoluteTime / ticksPerBeat;
                    var hand = MidiUtils.GetHand(noteOnEvent.NoteNumber, middleC);
                    allNotes.Add(new Tuple<double, int, string>(startBeat, noteOnEvent.NoteNumber, hand));
                }
                else if (midiEvent is NoteEvent noteEvent && (noteEvent is NoteOnEvent && noteEvent.Velocity == 0 || midiEvent.CommandCode == MidiCommandCode.NoteOff))
                {
                    var startNote = allNotes.FirstOrDefault(n => n.Item2 == noteEvent.NoteNumber);
                    if (startNote != null)
                    {
                        allNotes.Remove(startNote);
                        double startBeat = startNote.Item1;
                        double endBeat = noteEvent.AbsoluteTime / ticksPerBeat;
                        double durationBeats = endBeat - startBeat;
                        var hand = MidiUtils.GetHand(noteEvent.NoteNumber, middleC);
                        allNotes.Add(new Tuple<double, int, string>(startBeat, noteEvent.NoteNumber, hand));
                    }
                }
            }
        }

        // Group notes into measures
        foreach (var note in allNotes)
        {
            int measure = (int)(note.Item1 / beatsPerMeasure);
            if (!measures.ContainsKey(measure))
            {
                measures[measure] = new List<Tuple<double, int, string>>();
            }
            measures[measure].Add(note);
        }

        // Process each measure
        foreach (var measure in measures)
        {
            Console.WriteLine($"Measure {measure.Key + 1}:");
            var notesInMeasure = measure.Value;

            // Group notes into chords based on synchronization
            var chords = new List<Tuple<double, List<Tuple<int, string>>>>();

            foreach (var note in notesInMeasure)
            {
                //var matchingChords = chords.Where(chord => MidiUtils.AreNotesInSameChord(chord.Item1, note.Item1)).ToList();
                var matchingChords = chords.Where(chord => MidiUtils.AreNotesInSameChord(chord.Item1, note.Item1, chord.Item2.First().Item1, note.Item2)).ToList();

                if (matchingChords.Any())
                {
                    var chord = matchingChords.First();
                    chord.Item2.Add(new Tuple<int, string>(note.Item2, note.Item3));
                }
                else
                {
                    chords.Add(new Tuple<double, List<Tuple<int, string>>>(note.Item1, new List<Tuple<int, string>> { new Tuple<int, string>(note.Item2, note.Item3) }));
                }
            }

            // Print the synchronized notes and chords for this measure
            foreach (var chord in chords)
            {
                var notesNames = chord.Item2
                    .Select(note => MidiUtils.GetNoteName(note.Item1))
                    .ToList();
                string notesName = string.Join(", ", notesNames);
                double chordStartTime = chord.Item1;
                double chordEndTime = chord.Item2.Max(n => n.Item1); // Use max end time to calculate duration
                double durationBeats = chordEndTime - chordStartTime;
                string noteType = MidiUtils.GetNoteType(durationBeats, beatsPerMeasure);

                Console.WriteLine($"  {chord.Item2.First().Item2} - Notes: {notesName}, Start Time (beats): {chordStartTime}, Duration (beats): {durationBeats}, Type: {noteType}");
            }
        }
    }
    #endregion

    public static void ReadMidiFileWithChordAndSynchronize(string filePath)
    {
        var midiFile = new MidiFile(filePath, false);

        // Calculate Middle C dynamically
        //int middleC = 60; // MIDI note number for Middle C (C4)

        // Default tempo and time signature
        double tempoBPM = 120.0;
        int beatsPerMeasure = 4;
        int bottomSignature = 4;
        bool timeSignatureFound = false;

        // Find the tempo and time signature of the MIDI file
        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is TempoEvent tempoEvent)
                {
                    tempoBPM = tempoEvent.Tempo;
                }
                else if (midiEvent is TimeSignatureEvent timeSignatureEvent && !timeSignatureFound)
                {
                    beatsPerMeasure = timeSignatureEvent.Numerator;
                    bottomSignature = (int)Math.Pow(2, timeSignatureEvent.Denominator);
                    timeSignatureFound = true;
                    Console.WriteLine($"Initial time signature event: {beatsPerMeasure}/{bottomSignature}");
                }
            }
        }

        // Calculate the ticks per beat based on tempo
        double ticksPerBeat = midiFile.DeltaTicksPerQuarterNote;
        Console.WriteLine($"Ticks per beat: {ticksPerBeat}");

        // Maps to hold note on events and their start ticks
        var allNotes = new List<MidiNote>();
        var measures = new SortedDictionary<int, List<MidiNote>>();

        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is NoteOnEvent noteOnEvent && noteOnEvent.Velocity > 0)
                {
                    double startBeat = noteOnEvent.AbsoluteTime / ticksPerBeat;
                    //var hand = MidiUtils.GetHand(noteOnEvent.NoteNumber, middleC);
                    allNotes.Add(new MidiNote
                    {
                        StartTime = startBeat,
                        NoteNumber = noteOnEvent.NoteNumber,
                        //Hand = hand
                    });
                }
                else if (midiEvent is NoteEvent noteEvent && (noteEvent is NoteOnEvent && noteEvent.Velocity == 0 || midiEvent.CommandCode == MidiCommandCode.NoteOff))
                {
                    var startNote = allNotes.FirstOrDefault(n => n.NoteNumber == noteEvent.NoteNumber);
                    if (startNote != null)
                    {
                        allNotes.Remove(startNote);
                        double startBeat = startNote.StartTime;
                        double endBeat = noteEvent.AbsoluteTime / ticksPerBeat;
                        double durationBeats = endBeat - startBeat;
                        //var hand = MidiUtils.GetHand(noteEvent.NoteNumber, middleC);
                        allNotes.Add(new MidiNote
                        {
                            StartTime = startBeat,
                            NoteNumber = noteEvent.NoteNumber,
                            //Hand = hand
                        });
                    }
                }
            }
        }

        // Group notes into measures
        foreach (var note in allNotes)
        {
            int measure = (int)(note.StartTime / beatsPerMeasure);
            if (!measures.ContainsKey(measure))
            {
                measures[measure] = new List<MidiNote>();
            }
            measures[measure].Add(note);
        }

        // Process each measure
        foreach (var measure in measures)
        {
            Console.WriteLine($"Measure {measure.Key + 1}:");
            var notesInMeasure = measure.Value;

            // Group notes into chords based on synchronization
            var chords = new List<MidiChord>();

            foreach (MidiNote note in notesInMeasure)
            {
                List<MidiChord> matchingChords = chords.Where(chord => MidiUtils.AreNotesInSameChord(chord.StartTime, note.StartTime, chord.Notes.First().NoteNumber, note.NoteNumber)).ToList();

                if (matchingChords.Any())
                {
                    MidiChord chord = matchingChords.First();
                    chord.Notes.Add(note);
                }
                else
                {
                    chords.Add(new MidiChord
                    {
                        StartTime = note.StartTime,
                        Notes = new List<MidiNote> { note }
                    });
                }
            }

            // Print the synchronized notes and chords for this measure
            foreach (var chord in chords)
            {
                var notesNames = chord.Notes
                    .Select(n => MidiUtils.GetNoteName(n.NoteNumber))
                    .ToList();
                string notesName = string.Join(", ", notesNames);
                double chordEndTime = chord.Notes.Max(n => n.StartTime); // Use max end time to calculate duration
                double durationBeats = chordEndTime - chord.StartTime;
                string noteType = MidiUtils.GetNoteType(durationBeats, beatsPerMeasure);

                //Console.WriteLine($"  {chord.Notes.First().Hand} - Notes: {notesName}, Start Time (beats): {chord.StartTime}, Duration (beats): {durationBeats}, Type: {noteType}");
                Console.WriteLine($"  Notes: {notesName}, Start Time (beats): {chord.StartTime}, Duration (beats): {durationBeats}, Type: {noteType}");
            }
        }
    }

    public static void MyReadMidiFile(string filePath)
    {
        var midiFile = new MidiFile(filePath, false);

        // Calculate Middle C dynamically
        //int middleC = 60; // MIDI note number for Middle C (C4)

        // Default tempo and time signature
        double tempoBPM = 120.0;
        int beatsPerMeasure = 4;
        int bottomSignature = 4;
        bool timeSignatureFound = false;

        // Find the tempo and time signature of the MIDI file
        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is TempoEvent tempoEvent)
                {
                    tempoBPM = tempoEvent.Tempo;
                }
                else if (midiEvent is TimeSignatureEvent timeSignatureEvent && !timeSignatureFound)
                {
                    beatsPerMeasure = timeSignatureEvent.Numerator;
                    bottomSignature = (int)Math.Pow(2, timeSignatureEvent.Denominator);
                    timeSignatureFound = true;
                    Console.WriteLine($"Initial time signature event: {beatsPerMeasure}/{bottomSignature}");
                }
            }
        }

        // Calculate the ticks per beat based on tempo
        double ticksPerBeat = midiFile.DeltaTicksPerQuarterNote;
        Console.WriteLine($"Ticks per beat: {ticksPerBeat}");

        // Maps to hold note on events and their start ticks
        var allNotes = new List<MidiNote>();
        var measures = new SortedDictionary<int, List<MidiNote>>();

        foreach (var track in midiFile.Events)
        {
            foreach (var midiEvent in track)
            {
                if (midiEvent is NoteOnEvent noteOnEvent && noteOnEvent.Velocity > 0)
                {
                    double startBeat = noteOnEvent.AbsoluteTime / ticksPerBeat;
                    int deltaTime = noteOnEvent.DeltaTime;
                    int length = noteOnEvent.NoteLength;
                    //var hand = MidiUtils.GetHand(noteOnEvent.NoteNumber, middleC);
                    allNotes.Add(new MidiNote
                    {
                        StartTime = startBeat,
                        NoteNumber = noteOnEvent.NoteNumber,
                        Length = length,
                        DeltaTime = deltaTime,
                        //Hand = hand
                    });
                }
                else if (midiEvent is NoteEvent noteEvent && (noteEvent is NoteOnEvent && noteEvent.Velocity == 0 || midiEvent.CommandCode == MidiCommandCode.NoteOff))
                {
                    var startNote = allNotes.FirstOrDefault(n => n.NoteNumber == noteEvent.NoteNumber);
                    if (startNote != null)
                    {
                        allNotes.Remove(startNote);
                        double startBeat = startNote.StartTime;
                        double endBeat = noteEvent.AbsoluteTime / ticksPerBeat;
                        double durationBeats = endBeat - startBeat;
                        //var hand = MidiUtils.GetHand(noteEvent.NoteNumber, middleC);
                        allNotes.Add(new MidiNote
                        {
                            StartTime = startBeat,
                            NoteNumber = noteEvent.NoteNumber,
                            //Hand = hand
                        });
                    }
                }
            }
        }

        // Group notes into measures
        foreach (var note in allNotes)
        {
            int measure = (int)(note.StartTime / beatsPerMeasure);
            if (!measures.ContainsKey(measure))
            {
                measures[measure] = new List<MidiNote>();
            }
            measures[measure].Add(note);
        }

        // Process each measure
        string leftSybol = "";
        string rightSybol = "";

        foreach (var measure in measures)
        {
            Console.WriteLine($"Measure {measure.Key + 1}:");
            var notesInMeasure = measure.Value;

            // Group notes into chords based on synchronization
            var chords = new List<MidiChord>();

            foreach (MidiNote note in notesInMeasure)
            {
                List<MidiChord> sameTimeChords = chords.Where(chord => MidiUtils.AreNotesInSameTime(chord.StartTime, note.StartTime)).ToList();

                if (sameTimeChords.Any())
                {
                    List<MidiChord> matchingChords = chords.Where(chord => MidiUtils.AreNotesInSameChord(chord.StartTime, note.StartTime, chord.Notes.First().NoteNumber, note.NoteNumber)).ToList();
                    if (matchingChords.Any())
                    {
                        MidiChord chord = matchingChords.First();
                        chord.Notes.Add(note);
                    }
                    else
                    {
                        chords.Add(new MidiChord
                        {
                            StartTime = note.StartTime,
                            Notes = new List<MidiNote> { note },
                            Hand = MidiUtils.GetHand(note.NoteNumber, sameTimeChords.First().Notes.First().NoteNumber)
                        });
                    }
                }
                else
                {
                    chords.Add(new MidiChord
                    {
                        StartTime = note.StartTime,
                        Notes = new List<MidiNote> { note },
                        Hand = MidiUtils.GetHand(note.NoteNumber, MiddleC)
                    });
                }
            }

            // Print the synchronized notes and chords for this measure
            foreach (var chord in chords)
            {
                var notesNames = chord.Notes
                    .Select(n => MidiUtils.GetNoteName(n.NoteNumber))
                    .ToList();
                string notesName = string.Join(", ", notesNames);
                double chordEndTime = chord.Notes.Max(n => n.StartTime); // Use max end time to calculate duration
                double durationBeats = chordEndTime - chord.StartTime;
                string noteType = MidiUtils.GetNoteType(durationBeats, beatsPerMeasure);
                double noteTypeDuration = MidiUtils.GetNoteTypeDuration(durationBeats, beatsPerMeasure);

                //Console.WriteLine($"  {chord.Notes.First().Hand} - Notes: {notesName}, Start Time (beats): {chord.StartTime}, Duration (beats): {durationBeats}, Type: {noteType}");
                Console.WriteLine($"  {chord.Hand} - Notes: {notesName}, Type: {noteType}, Duration (beats): {durationBeats}, Type: {noteType}, Start Time (beats): {chord.StartTime}");
                if(chord.Hand == "Left Hand")
                {
                    leftSybol += $"{notesName}_{noteTypeDuration} ";
                }
                else
                {
                    rightSybol += $"{notesName}_{noteTypeDuration} ";
                }
            }
            leftSybol += "/";
            rightSybol += "/";
        }
        Console.WriteLine();
        Console.WriteLine("Left symbol: " + leftSybol);
        Console.WriteLine();
        Console.WriteLine("Right symbol: " + rightSybol);
        Console.ReadLine();
    }


    #endregion
}
