using System;
using System.Collections.Generic;

public class Note
{
    public string Name { get; set; }
    public string Type { get; set; } // whole, half, quarter, eighth, sixteenth, etc.
}

public class MeasureValidator
{
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

    public static void Main()
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
}
