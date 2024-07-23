using DataLayer.DbObject;
using DataLayer.EnumsAndConsts;
using DataLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ServiceLayer.Seed
{
    public static class DbSeed
    {
        //public static string[] Roles = { "Admin", "Artist", "Player" };
        public static Role[] Roles = new[]
        {
            new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new Role { Id = 2, Name = "Artist", NormalizedName = "ARTIST" },
            new Role { Id = 3, Name = "Player", NormalizedName = "PLAYER" }
        };
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

            //1b
            new Note { Id = 57, Name = "C1b", Octave = 1, Pitch = "C1b" },
            new Note { Id = 58, Name = "D1b", Octave = 1, Pitch = "D1b" },
            new Note { Id = 59, Name = "E1b", Octave = 1, Pitch = "E1b" },
            new Note { Id = 60, Name = "F1b", Octave = 1, Pitch = "F1b" },
            new Note { Id = 61, Name = "G1b", Octave = 1, Pitch = "G1b" },
            new Note { Id = 62, Name = "A1b", Octave = 1, Pitch = "A1b" },
            new Note { Id = 63, Name = "B1b", Octave = 1, Pitch = "B1b" },

            //2b
            new Note { Id = 64, Name = "C2b", Octave = 2, Pitch = "C2b" },
            new Note { Id = 65, Name = "D2b", Octave = 2, Pitch = "D2b" },
            new Note { Id = 66, Name = "E2b", Octave = 2, Pitch = "E2b" },
            new Note { Id = 67, Name = "F2b", Octave = 2, Pitch = "F2b" },
            new Note { Id = 68, Name = "G2b", Octave = 2, Pitch = "G2b" },
            new Note { Id = 69, Name = "A2b", Octave = 2, Pitch = "A2b" },
            new Note { Id = 70, Name = "B2b", Octave = 2, Pitch = "B2b" },

            //3b
            new Note { Id = 71, Name = "C3b", Octave = 3, Pitch = "C3b" },
            new Note { Id = 72, Name = "D3b", Octave = 3, Pitch = "D3b" },
            new Note { Id = 73, Name = "E3b", Octave = 3, Pitch = "E3b" },
            new Note { Id = 74, Name = "F3b", Octave = 3, Pitch = "F3b" },
            new Note { Id = 75, Name = "G3b", Octave = 3, Pitch = "G3b" },
            new Note { Id = 76, Name = "A3b", Octave = 3, Pitch = "A3b" },
            new Note { Id = 77, Name = "B3b", Octave = 3, Pitch = "B3b" },

            //4b
            new Note { Id = 78, Name = "C4b", Octave = 4, Pitch = "C4b" },
            new Note { Id = 79, Name = "D4b", Octave = 4, Pitch = "D4b" },
            new Note { Id = 80, Name = "E4b", Octave = 4, Pitch = "E4b" },
            new Note { Id = 81, Name = "F4b", Octave = 4, Pitch = "F4b" },
            new Note { Id = 82, Name = "G4b", Octave = 4, Pitch = "G4b" },
            new Note { Id = 83, Name = "A4b", Octave = 4, Pitch = "A4b" },
            new Note { Id = 84, Name = "B4b", Octave = 4, Pitch = "B4b" },
            
            //5b
            new Note { Id = 85, Name = "C5b", Octave = 5, Pitch = "C5b" },
            new Note { Id = 86, Name = "D5b", Octave = 5, Pitch = "D5b" },
            new Note { Id = 87, Name = "E5b", Octave = 5, Pitch = "E5b" },
            new Note { Id = 88, Name = "F5b", Octave = 5, Pitch = "F5b" },
            new Note { Id = 89, Name = "G5b", Octave = 5, Pitch = "G5b" },
            new Note { Id = 90, Name = "A5b", Octave = 5, Pitch = "A5b" },
            new Note { Id = 91, Name = "B5b", Octave = 5, Pitch = "B5b" },
            
            //6b
            new Note { Id = 92, Name = "C6b", Octave = 6, Pitch = "C6b" },
            new Note { Id = 93, Name = "D6b", Octave = 6, Pitch = "D6b" },
            new Note { Id = 94, Name = "E6b", Octave = 6, Pitch = "E6b" },
            new Note { Id = 95, Name = "F6b", Octave = 6, Pitch = "F6b" },
            new Note { Id = 96, Name = "G6b", Octave = 6, Pitch = "G6b" },
            new Note { Id = 97, Name = "A6b", Octave = 6, Pitch = "A6b" },
            new Note { Id = 98, Name = "B6b", Octave = 6, Pitch = "B6b" },

            //7b
            new Note { Id = 99, Name = "C7b", Octave = 7, Pitch = "C7b" },
            new Note { Id = 100, Name = "D7b", Octave = 7, Pitch = "D7b" },
            new Note { Id = 101, Name = "E7b", Octave = 7, Pitch = "E7b" },
            new Note { Id = 102, Name = "F7b", Octave = 7, Pitch = "F7b" },
            new Note { Id = 103, Name = "G7b", Octave = 7, Pitch = "G7b" },
            new Note { Id = 104, Name = "A7b", Octave = 7, Pitch = "A7b" },
            new Note { Id = 105, Name = "B7b", Octave = 7, Pitch = "B7b" },

            //8b
            new Note { Id = 106, Name = "C8b", Octave = 8, Pitch = "C8b" },
            new Note { Id = 107, Name = "D8b", Octave = 8, Pitch = "D8b" },
            new Note { Id = 108, Name = "E8b", Octave = 8, Pitch = "E8b" },
            new Note { Id = 109, Name = "F8b", Octave = 8, Pitch = "F8b" },
            new Note { Id = 110, Name = "G8b", Octave = 8, Pitch = "G8b" },
            new Note { Id = 111, Name = "A8b", Octave = 8, Pitch = "A8b" },
            new Note { Id = 112, Name = "B8b", Octave = 8, Pitch = "B8b" },

            //1#
            new Note { Id = 113, Name = "C1#", Octave = 1, Pitch = "C1#" },
            new Note { Id = 114, Name = "D1#", Octave = 1, Pitch = "D1#" },
            new Note { Id = 115, Name = "E1#", Octave = 1, Pitch = "E1#" },
            new Note { Id = 116, Name = "F1#", Octave = 1, Pitch = "F1#" },
            new Note { Id = 117, Name = "G1#", Octave = 1, Pitch = "G1#" },
            new Note { Id = 118, Name = "A1#", Octave = 1, Pitch = "A1#" },
            new Note { Id = 119, Name = "B1#", Octave = 1, Pitch = "B1#" },

            //2#
            new Note { Id = 120, Name = "C2#", Octave = 2, Pitch = "C2#" },
            new Note { Id = 121, Name = "D2#", Octave = 2, Pitch = "D2#" },
            new Note { Id = 122, Name = "E2#", Octave = 2, Pitch = "E2#" },
            new Note { Id = 123, Name = "F2#", Octave = 2, Pitch = "F2#" },
            new Note { Id = 124, Name = "G2#", Octave = 2, Pitch = "G2#" },
            new Note { Id = 125, Name = "A2#", Octave = 2, Pitch = "A2#" },
            new Note { Id = 126, Name = "B2#", Octave = 2, Pitch = "B2#" },

            //3#
            new Note { Id = 127, Name = "C3#", Octave = 3, Pitch = "C3#" },
            new Note { Id = 128, Name = "D3#", Octave = 3, Pitch = "D3#" },
            new Note { Id = 129, Name = "E3#", Octave = 3, Pitch = "E3#" },
            new Note { Id = 130, Name = "F3#", Octave = 3, Pitch = "F3#" },
            new Note { Id = 131, Name = "G3#", Octave = 3, Pitch = "G3#" },
            new Note { Id = 132, Name = "A3#", Octave = 3, Pitch = "A3#" },
            new Note { Id = 133, Name = "B3#", Octave = 3, Pitch = "B3#" },

            //4#
            new Note { Id = 134, Name = "C4#", Octave = 4, Pitch = "C4#" },
            new Note { Id = 135, Name = "D4#", Octave = 4, Pitch = "D4#" },
            new Note { Id = 136, Name = "E4#", Octave = 4, Pitch = "E4#" },
            new Note { Id = 137, Name = "F4#", Octave = 4, Pitch = "F4#" },
            new Note { Id = 138, Name = "G4#", Octave = 4, Pitch = "G4#" },
            new Note { Id = 139, Name = "A4#", Octave = 4, Pitch = "A4#" },
            new Note { Id = 140, Name = "B4#", Octave = 4, Pitch = "B4#" },

            //5#
            new Note { Id = 141, Name = "C5#", Octave = 5, Pitch = "C5#" },
            new Note { Id = 142, Name = "D5#", Octave = 5, Pitch = "D5#" },
            new Note { Id = 143, Name = "E5#", Octave = 5, Pitch = "E5#" },
            new Note { Id = 144, Name = "F5#", Octave = 5, Pitch = "F5#" },
            new Note { Id = 145, Name = "G5#", Octave = 5, Pitch = "G5#" },
            new Note { Id = 146, Name = "A5#", Octave = 5, Pitch = "A5#" },
            new Note { Id = 147, Name = "B5#", Octave = 5, Pitch = "B5#" },

            //6#
            new Note { Id = 148, Name = "C6#", Octave = 6, Pitch = "C6#" },
            new Note { Id = 149, Name = "D6#", Octave = 6, Pitch = "D6#" },
            new Note { Id = 150, Name = "E6#", Octave = 6, Pitch = "E6#" },
            new Note { Id = 151, Name = "F6#", Octave = 6, Pitch = "F6#" },
            new Note { Id = 152, Name = "G6#", Octave = 6, Pitch = "G6#" },
            new Note { Id = 153, Name = "A6#", Octave = 6, Pitch = "A6#" },
            new Note { Id = 154, Name = "B6#", Octave = 6, Pitch = "B6#" },

            //7#
            new Note { Id = 155, Name = "C7#", Octave = 7, Pitch = "C7#" },
            new Note { Id = 156, Name = "D7#", Octave = 7, Pitch = "D7#" },
            new Note { Id = 157, Name = "E7#", Octave = 7, Pitch = "E7#" },
            new Note { Id = 158, Name = "F7#", Octave = 7, Pitch = "F7#" },
            new Note { Id = 159, Name = "G7#", Octave = 7, Pitch = "G7#" },
            new Note { Id = 160, Name = "A7#", Octave = 7, Pitch = "A7#" },
            new Note { Id = 161, Name = "B7#", Octave = 7, Pitch = "B7#" },

            //8#
            new Note { Id = 162, Name = "C8#", Octave = 8, Pitch = "C8#" },
            new Note { Id = 163, Name = "D8#", Octave = 8, Pitch = "D8#" },
            new Note { Id = 164, Name = "E8#", Octave = 8, Pitch = "E8#" },
            new Note { Id = 165, Name = "F8#", Octave = 8, Pitch = "F8#" },
            new Note { Id = 166, Name = "G8#", Octave = 8, Pitch = "G8#" },
            new Note { Id = 167, Name = "A8#", Octave = 8, Pitch = "A8#" },
            new Note { Id = 168, Name = "B8#", Octave = 8, Pitch = "B8#" },

            //Pause
            new Note { Id = 169, Name = "Pause", Octave = 8, Pitch = "Pause" }
        };

        public static User[] Admins = new User[]
        {
            new User
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@gmail.com",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
                //PasswordHash = "123456789",
            }
        };

        public static User[] Artists = new User[]
        {
            new User
            {
                Id = 2,
                Email = "artist1@gmail.com",
                UserName = "artist1",
                NormalizedUserName = "ARTIST1",
                NormalizedEmail = "ARTIST1@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
                 //PasswordHash = "123456789",
            },
            new User
            {
                Id = 3,
                Email = "artist2@gmail.com",
                UserName = "artist2",
                NormalizedUserName = "ARTIST2",
                NormalizedEmail = "ARTIST2@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
                 //PasswordHash = "123456789",
            },
        };

        public static User[] Players = new User[]
        {
            new User
            {
                 Id = 4,
                 Email = "player1@gmail.com",
                 UserName = "player1",
                 //PasswordHash = "123456789",
            }   ,
            new User
            {
                 Id = 5,
                 Email = "player2@gmail.com",
                 UserName = "player2",
                 //PasswordHash = "123456789",
            }
        };

        public static Genre[] Genres = new Genre[]
        {
            new Genre { Id = 1, Name = "Country" },
            new Genre { Id = 2, Name = "Balad" }
        };

        public static Song[] Songs = new Song[]
        {
            new Song
            {
                Id=1,
                ArtistId = 2,
                Composer = "artist1",
                GenreId=2,
                Title="Song 1",

            }   ,
            new Song
            {
                Id=2,
                ArtistId = 3,
                Composer = "artist2",
                GenreId=2,
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
                 TopSignature = 4,
                 BottomSignature = 4,
             },
             new Sheet
             {
                 Id = 2,
                 InstrumentId = 2,
                 SongId = 1,
                 SheetFile = "",
                 TopSignature = 4,
                 BottomSignature = 4,
             },
             new Sheet
             {
                 Id = 3,
                 InstrumentId = 2,
                 SongId = 2,
                 SheetFile = "",
                 TopSignature = 4,
                 BottomSignature = 4,
             },
             new Sheet
             {
                 Id = 4,
                 InstrumentId = 1,
                 SongId = 1,
                 SheetFile = "https://firebasestorage.googleapis.com/v0/b/pianoaiapi.appspot.com/o/Midi%2Ff1d4cb7b-9e3b-445e-a3e7-f97fc78e5434_Sao_Sang.mid?alt=media&token=fb758635-1027-43cc-bbff-1a0db24177bb",
                 TopSignature = 4,
                 BottomSignature = 4,
             },

        };

        public static Measure[] Measures = new Measure[]
        {
            new Measure
            {
                Id = 1,
                RightSheetId = 1,
                Position = 1,
            },
            new Measure
            {
                Id = 2,
                RightSheetId = 1,
                Position = 2,
            } ,
            new Measure
            {
                Id = 3,
                RightSheetId = 2,
                Position = 1,
            },
             new Measure
            {
                Id = 4,
                RightSheetId = 3,
                Position = 1,
            }  ,
             new Measure
            {
                Id = 5,
                LeftSheetId = 3,
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
            },
            new Chord
            {
                Id = 2,
                Duration = 2,
                MeasureId = 2,
                Position = 1,
            },
            new Chord
            {
                Id = 3,
                Duration = 2,
                MeasureId = 2,
                Position = 2,
            },
            new Chord
            {
                Id = 4,
                Duration = 1,
                MeasureId = 3,
                Position = 1,
            },
            new Chord
            {
                Id = 5,
                Duration = 1,
                MeasureId = 3,
                Position = 2,
            },
            new Chord
            {
                Id = 6,
                Duration = 1,
                MeasureId = 3,
                Position = 3,
            },
            new Chord
            {
                Id = 7,
                Duration = 1,
                MeasureId = 3,
                Position = 4,
            },
            new Chord
            {
                Id = 8,
                Duration = 4,
                MeasureId = 4,
                Position = 1,
            },
            new Chord
            {
                Id = 9,
                Duration = 4,
                MeasureId = 5,
                Position = 1,
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
            new ChordNote
            {
                Id = 9,
                ChordId = 9,
                NoteId = 4,
            },
        };
    }
}
