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
           new Note
           {
               Id = 1,
               Name = PitchConst.C,
               Octave = (int)OctaveEnum.Mid,
               Pitch = PitchConst.C,
           } ,
           new Note
           {
               Id = 2,
               Name = PitchConst.D,
               Octave = (int)OctaveEnum.Mid,
               Pitch = PitchConst.D,
           },
           new Note
           {
               Id = 3,
               Name = PitchConst.E,
               Octave = (int)OctaveEnum.Mid,
               Pitch = PitchConst.E,
           },
           new Note
           {
               Id = 4,
               Name = PitchConst.F,
               Octave = 4,
               Pitch = PitchConst.F,
           },
           new Note
           {
               Id = 5,
               Name = PitchConst.G,
               Octave = (int)OctaveEnum.Mid,
               Pitch = PitchConst.G,
           },
           new Note
           {
               Id = 6,
               Name = PitchConst.A,
               Octave = (int)OctaveEnum.Mid,
               Pitch = PitchConst.A,
           },
           new Note
           {
               Id = 7,
               Name = PitchConst.B,
               Octave = (int)OctaveEnum.Mid,
               Pitch = PitchConst.B,
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
                SheetId = 1,
                Position = 3,
            }
        };
        public static SongNote[] SongNotes = new SongNote[]
        {
            new SongNote
            {
                Id = 1,
                NoteID = 1,
                Duration = 4,
                MeasureId = 1,
                Position = 1,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new SongNote
            {
                Id = 2,
                NoteID = 2,
                Duration = 2,
                MeasureId = 2,
                Position = 1,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new SongNote
            {
                Id = 3,
                NoteID = 3,
                Duration = 2,
                MeasureId = 2,
                Position = 2,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new SongNote
            {
                Id = 4,
                NoteID = 4,
                Duration = 1,
                MeasureId = 3,
                Position = 1,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new SongNote
            {
                Id = 5,
                NoteID = 4,
                Duration = 1,
                MeasureId = 3,
                Position = 2,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new SongNote
            {
                Id = 6,
                NoteID = 4,
                Duration = 1,
                MeasureId = 3,
                Position = 3,
                Chromatic = (int)ChromaticEnum.Natural
            },
            new SongNote
            {
                Id = 7,
                NoteID = 4,
                Duration = 1,
                MeasureId = 3,
                Position = 4,
                Chromatic = (int)ChromaticEnum.Natural
            },
        };
    }
}
