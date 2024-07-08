using DataLayer.DbObject;
using DataLayer.EnumsAndConsts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Seed
{
    public static class DbSeed
    {
        public static Instrument[] Instruments = new Instrument[]
        {
            new Instrument
            {
                Id    = 1,
                Name = "Piano"
            },
            new Instrument
            {
                Id    = 2,
                Name = "Guitar"
            }
        };
        public static Note[] Notes = new Note[]
        {
            // Octave 1
            new Note { Id = 1, Name = "C1", Octave = 1, Pitch = "C1" },
            new Note { Id = 2, Name = "D1", Octave = 1, Pitch = "D1" },
            new Note { Id = 3, Name = "E1", Octave = 1, Pitch = "E1" },
            new Note { Id = 4, Name = "F1", Octave = 1, Pitch = "F1" },
            new Note { Id = 5, Name = "G1", Octave = 1, Pitch = "G1" },
            new Note { Id = 6, Name = "A1", Octave = 1, Pitch = "A1" },
            new Note { Id = 7, Name = "B1", Octave = 1, Pitch = "B1" },

            // Octave 2
            new Note { Id = 8, Name = "C2", Octave = 2, Pitch = "C2" },
            new Note { Id = 9, Name = "D2", Octave = 2, Pitch = "D2" },
            new Note { Id = 10, Name = "E2", Octave = 2, Pitch = "E2" },
            new Note { Id = 11, Name = "F2", Octave = 2, Pitch = "F2" },
            new Note { Id = 12, Name = "G2", Octave = 2, Pitch = "G2" },
            new Note { Id = 13, Name = "A2", Octave = 2, Pitch = "A2" },
            new Note { Id = 14, Name = "B2", Octave = 2, Pitch = "B2" },

            // Octave 3
            new Note { Id = 15, Name = "C3", Octave = 3, Pitch = "C3" },
            new Note { Id = 16, Name = "D3", Octave = 3, Pitch = "D3" },
            new Note { Id = 17, Name = "E3", Octave = 3, Pitch = "E3" },
            new Note { Id = 18, Name = "F3", Octave = 3, Pitch = "F3" },
            new Note { Id = 19, Name = "G3", Octave = 3, Pitch = "G3" },
            new Note { Id = 20, Name = "A3", Octave = 3, Pitch = "A3" },
            new Note { Id = 21, Name = "B3", Octave = 3, Pitch = "B3" },

            // Octave 4
            new Note { Id = 22, Name = "C4", Octave = 4, Pitch = "C4" },
            new Note { Id = 23, Name = "D4", Octave = 4, Pitch = "D4" },
            new Note { Id = 24, Name = "E4", Octave = 4, Pitch = "E4" },
            new Note { Id = 25, Name = "F4", Octave = 4, Pitch = "F4" },
            new Note { Id = 26, Name = "G4", Octave = 4, Pitch = "G4" },
            new Note { Id = 27, Name = "A4", Octave = 4, Pitch = "A4" },
            new Note { Id = 28, Name = "B4", Octave = 4, Pitch = "B4" },

            // Octave 5
            new Note { Id = 29, Name = "C5", Octave = 5, Pitch = "C5" },
            new Note { Id = 30, Name = "D5", Octave = 5, Pitch = "D5" },
            new Note { Id = 31, Name = "E5", Octave = 5, Pitch = "E5" },
            new Note { Id = 32, Name = "F5", Octave = 5, Pitch = "F5" },
            new Note { Id = 33, Name = "G5", Octave = 5, Pitch = "G5" },
            new Note { Id = 34, Name = "A5", Octave = 5, Pitch = "A5" },
            new Note { Id = 35, Name = "B5", Octave = 5, Pitch = "B5" },

            // Octave 6
            new Note { Id = 36, Name = "C6", Octave = 6, Pitch = "C6" },
            new Note { Id = 37, Name = "D6", Octave = 6, Pitch = "D6" },
            new Note { Id = 38, Name = "E6", Octave = 6, Pitch = "E6" },
            new Note { Id = 39, Name = "F6", Octave = 6, Pitch = "F6" },
            new Note { Id = 40, Name = "G6", Octave = 6, Pitch = "G6" },
            new Note { Id = 41, Name = "A6", Octave = 6, Pitch = "A6" },
            new Note { Id = 42, Name = "B6", Octave = 6, Pitch = "B6" },

            // Octave 7
            new Note { Id = 43, Name = "C7", Octave = 7, Pitch = "C7" },
            new Note { Id = 44, Name = "D7", Octave = 7, Pitch = "D7" },
            new Note { Id = 45, Name = "E7", Octave = 7, Pitch = "E7" },
            new Note { Id = 46, Name = "F7", Octave = 7, Pitch = "F7" },
            new Note { Id = 47, Name = "G7", Octave = 7, Pitch = "G7" },
            new Note { Id = 48, Name = "A7", Octave = 7, Pitch = "A7" },
            new Note { Id = 49, Name = "B7", Octave = 7, Pitch = "B7" },

            // Octave 8
            new Note { Id = 50, Name = "C8", Octave = 8, Pitch = "C8" },
            new Note { Id = 51, Name = "D8", Octave = 8, Pitch = "D8" },
            new Note { Id = 52, Name = "E8", Octave = 8, Pitch = "E8" },
            new Note { Id = 53, Name = "F8", Octave = 8, Pitch = "F8" },
            new Note { Id = 54, Name = "G8", Octave = 8, Pitch = "G8" },
            new Note { Id = 55, Name = "A8", Octave = 8, Pitch = "A8" },
            new Note { Id = 56, Name = "B8", Octave = 8, Pitch = "B8" },

            //Pause
            new Note { Id = 57, Name = "Pause", Octave = 8, Pitch = "Pause" }
        };
        public static User[] Artists = new User[]
        {
            new User
            {
                 Id = 1,
                 Email = "artist1",
                 Username = "artist1@gmail.com",
                 Password = "123456789",
            }   ,
            new User
            {
                 Id = 2,
                 Email = "artist2",
                 Username = "artist2@gmail.com",
                 Password = "123456789",
            }
        };

        public static Song[] Songs = new Song[]
        {
            new Song
            {
                Id=1,
                ArtistId = 1,
                Composer = "artist1",
                Genre="Country",
                Title="Song 1",
            }   ,
            new Song
            {
                Id=2,
                ArtistId = 2,
                Composer = "artist2",
                Genre="Country",
                Title="Song 2",
            }
        };
        public static Sheet[] Sheets = new Sheet[]
        {
             new Sheet
             {
                 Id = 1,
                 InstrumentId = 1,
                 SongId = 1,
                 SheetFile = "",
             },
             new Sheet
             {
                 Id = 2,
                 InstrumentId = 2,
                 SongId = 1,
                 SheetFile = "",
             },
             new Sheet
             {
                 Id = 3,
                 InstrumentId = 2,
                 SongId = 2,
                 SheetFile = "",
             }
        };
        public static Measure[] Measures = new Measure[]
        {
            new Measure
            {
                Id = 1,
                SheetId = 1,
                Position = 1,
            },
            new Measure
            {
                Id = 2,
                SheetId = 1,
                Position = 2,
            } ,
            new Measure
            {
                Id = 3,
                SheetId = 2,
                Position = 1,
            },
             new Measure
            {
                Id = 4,
                SheetId = 3,
                Position = 1,
            }
        };
        public static Chord[] Chords = new Chord[]
        {
            new Chord
            {
                Id = 1,
                Duration = 4,
                MeasureId = 1,
                Position = 1,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new Chord
            {
                Id = 2,
                Duration = 2,
                MeasureId = 2,
                Position = 1,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new Chord
            {
                Id = 3,
                Duration = 2,
                MeasureId = 2,
                Position = 2,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new Chord
            {
                Id = 4,
                Duration = 1,
                MeasureId = 3,
                Position = 1,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new Chord
            {
                Id = 5,
                Duration = 1,
                MeasureId = 3,
                Position = 2,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new Chord
            {
                Id = 6,
                Duration = 1,
                MeasureId = 3,
                Position = 3,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new Chord
            {
                Id = 7,
                Duration = 1,
                MeasureId = 3,
                Position = 4,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new Chord
            {
                Id = 8,
                Duration = 4,
                MeasureId = 4,
                Position = 1,
                Chromatic = (int)ChromaticEnum.Natural
            },
        };
        public static ChordNote[] ChordNotes = new ChordNote[]
        {
            new ChordNote
            {
                Id = 1,
                ChordId = 1,
                NoteId = PitchConst.C4id,
            },
            new ChordNote
            {
                Id = 2,
                ChordId = 2,
                NoteId = PitchConst.D4id,
            },
            new ChordNote
            {
                Id = 3,
                ChordId = 3,
                NoteId = PitchConst.E4id,
            },
            new ChordNote
            {
                Id = 4,
                ChordId = 4,
                NoteId = 4,
            },
            new ChordNote
            {
                Id = 5,
                ChordId = 5,
                NoteId = 4,
            },
            new ChordNote
            {
                Id = 6,
                ChordId = 6,
                NoteId = 4,
            },
            new ChordNote
            {
                Id = 7,
                ChordId = 7,
                NoteId = 4,
            },
            new ChordNote
            {
                Id = 8,
                ChordId = 8,
                NoteId = 4,
            },
        };
    }
}
