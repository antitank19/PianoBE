﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.EnumsAndConsts;

namespace DataLayer.DbObject
{
    public class ChordNote
    {
        public ChordNote() { }
        public ChordNote(string noteInfo)
        {
              FillPitch(noteInfo);
            FillOctave(noteInfo);   
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int NoteId { get; set; }
        public Note Note { get; set; }
        public int ChordId  { get; set; }
        public Chord Chord  { get; set; }

        public void FillPitch(string noteInfo)
        {
            string pitch = noteInfo.Substring(0, 1);
            switch (pitch)
            {
                case ("C"):
                    NoteId = PitchConst.C4id;
                    break;
                case ("D"):
                    NoteId = PitchConst.D4id;
                    break;
                case ("E"):
                    NoteId = PitchConst.E4id;
                    break;
                case ("F"):
                    NoteId = PitchConst.F4id;
                    break;
                case ("G"):
                    NoteId = PitchConst.G4id;
                    break;
                case ("A"):
                    NoteId = PitchConst.A4id;
                    break;
                case ("B"):
                    NoteId = PitchConst.B4id;
                    break;
                case ("-"):
                    NoteId = PitchConst.PauseId;
                    break;
            }
            #region old code
            //if (noteInfo.IndexOf('A') != -1)
            //{
            //    ThePitch = Pitch.A;
            //}
            //if (noteInfo.IndexOf('B') != -1)
            //{
            //    ThePitch = Pitch.B;
            //}
            //if (noteInfo.IndexOf('C') != -1)
            //{
            //    ThePitch = Pitch.C;
            //}
            //if (noteInfo.IndexOf('D') != -1)
            //{
            //    ThePitch = Pitch.D;
            //}
            //if (noteInfo.IndexOf('E') != -1)
            //{
            //    ThePitch = Pitch.E;
            //}
            //if (noteInfo.IndexOf('F') != -1)
            //{
            //    ThePitch = Pitch.F;
            //}
            //if (noteInfo.IndexOf('G') != -1)
            //{
            //    ThePitch = Pitch.G;
            //}
            #endregion

        }
        public void FillOctave(string noteInfo)
        {
            #region old code
            //if (NoteInfo.IndexOf('l') != -1)
            //{
            //    TheOctave = Octave.low;
            //}
            //if (NoteInfo.IndexOf('m') != -1)
            //{
            //    TheOctave = Octave.middle;
            //}
            //if (NoteInfo.IndexOf('h') != -1)
            //{
            //    TheOctave = Octave.high;
            //}
            #endregion
            string octaveString = noteInfo.Substring(1, 1);
            int octaveInt = Int32.Parse(octaveString);
            //Khi lưu cao độ là lưu theo của khoảng 4 trc, giờ trừ
            NoteId += (4 - octaveInt) * 7;

        }

    }

    public class WrongNoteStringFormatException : Exception
    {

        public WrongNoteStringFormatException(int measurePos = 1, int notePos = 1)
        {
            MeasurePos = measurePos;
            NotePos = notePos;
            CustomMessage = $"Sai cú pháp ở khuôn nhạc thứ {measurePos}, nốt thứ {notePos}";
        }
        public string CustomMessage { get; set; }
        public int MeasurePos { get; set; }

        public int NotePos { get; set; }

    }
}
