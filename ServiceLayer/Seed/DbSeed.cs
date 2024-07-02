using DataLayer.DbObject;
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
           new Note
           {
               Id = 1,
               Name = "C4",
               Octave = 4,
               Pitch = "C4",
           } ,
           new Note
           {
               Id = 2,
               Name = "D4",
               Octave = 4,
               Pitch = "D4",
           },
           new Note
           {
               Id = 3,
               Name = "E4",
               Octave = 4,
               Pitch = "E4",
           },
           new Note
           {
               Id = 4,
               Name = "F4",
               Octave = 4,
               Pitch = "F4",
           },
           new Note
           {
               Id = 5,
               Name = "G4",
               Octave = 4,
               Pitch = "G4",
           },
           new Note
           {
               Id = 6,
               Name = "A4",
               Octave = 4,
               Pitch = "A4",
           },
           new Note
           {
               Id = 7,
               Name = "B4",
               Octave = 4,
               Pitch = "B4",
           }
        };
        public static Artist[] Artists = new Artist[]
        {
            new Artist
            {
                 Id = 1,
                 Email = "artist1",
                 Username = "artist1@gmail.com",
                 Password = "123456789",
            }   ,
            new Artist
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
             }
        };
        public static SongNote[] SongNotes = new SongNote[]
        {
            new SongNote
            {
                Id = 1,
                SheetID = 1,
                NoteID = 1,
                Duration = 4,
                Measure = 1,
                Position = 1,
            },
            new SongNote
            {
                Id = 2,
                SheetID = 1,
                NoteID = 2,
                Duration = 2,
                Measure = 2,
                Position = 1,
            },
            new SongNote
            {
                Id = 3,
                SheetID = 1,
                NoteID = 3,
                Duration = 2,
                Measure = 2,
                Position = 2,
            },
            new SongNote
            {
                Id = 4,
                SheetID = 1,
                NoteID = 4,
                Duration = 1,
                Measure = 3,
                Position = 1,
            },
            new SongNote
            {
                Id = 5,
                SheetID = 1,
                NoteID = 4,
                Duration = 1,
                Measure = 3,
                Position = 2,
            },
            new SongNote
            {
                Id = 6,
                SheetID = 1,
                NoteID = 4,
                Duration = 1,
                Measure = 3,
                Position = 3,
            },
            new SongNote
            {
                Id = 7,
                SheetID = 1,
                NoteID = 4,
                Duration = 1,
                Measure = 3,
                Position = 4,
            },
        };
    }
}
