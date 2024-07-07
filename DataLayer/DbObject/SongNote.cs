using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.EnumsAndConsts;

namespace DataLayer.DbObject
{
    public class SongNote
    {
        public SongNote()
        {

        }
        /// <summary>
        /// Create a music sheet using string
        /// </summary>
        /// <param name="measureId"></param>
        /// <param name="noteString">
        /// (Note)(Octave)(Chromatic)_(Duration)
        ///     Note: C D E F G A B = do re mi fa sol la si, - for pause
        ///     Octave: 1-8 with 4 is the ussual
        ///     Chormatic: # for sharp, b for flat
        /// </param>
        public SongNote(int measureId, int position, string noteString)
        {
            try
            {
                MeasureId = measureId;
                Position = position;
                FillPitch(noteString);
                if (NoteID == PitchConst.PauseId)
                {
                    Chromatic = (int)ChromaticEnum.Natural;
                }
                else
                {
                    FillOctave(noteString);
                    FillChromatic(noteString);
                }
                FillDuration(noteString);
            }
            catch (Exception ex)
            {
                if (ex is WrongNoteStringFormatException) throw ex;
            }
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int NoteID { get; set; }
        public Note Note { get; set; }
        public float Duration { get; set; }
        // Flat:0, Thường:1, Sharp: 2
        public int Chromatic { get; set; } = (int)ChromaticEnum.Natural;
        //Vị trí nốt nhạc
        //Measure: thứ tự khuôn nhạc
        //Position: thứ tự note trong khuôn nhạc
        public int MeasureId { get; set; }
        public Measure Measure { get; set; }

        public int Position { get; set; }

        public void FillPitch(string noteInfo)
        {
            string pitch = noteInfo.Substring(0, 1);
            switch (pitch)
            {
                case ("C"):
                    NoteID = PitchConst.C4id;
                    break;
                case ("D"):
                    NoteID = PitchConst.D4id;
                    break;
                case ("E"):
                    NoteID = PitchConst.E4id;
                    break;
                case ("F"):
                    NoteID = PitchConst.F4id;
                    break;
                case ("G"):
                    NoteID = PitchConst.G4id;
                    break;
                case ("A"):
                    NoteID = PitchConst.A4id;
                    break;
                case ("B"):
                    NoteID = PitchConst.B4id;
                    break;
                case ("-"):
                    NoteID = PitchConst.PauseId;
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
            NoteID += (4 - octaveInt) * 7;

        }

        public void FillDuration(string NoteInfo)
        {
            string duartionString = NoteInfo.Split('_')[1];
            Duration = float.Parse(duartionString);
            #region old code
            //if (NoteInfo.IndexOf('x') != -1)
            //{
            //    Duration = DurationConst.sixteenth;
            //}
            //if (NoteInfo.IndexOf('e') != -1)
            //{
            //    Duration = Timing.eighth;
            //}
            //if (NoteInfo.IndexOf('q') != -1)
            //{
            //    Duration = Timing.quarter;
            //}
            //if (NoteInfo.IndexOf('a') != -1)
            //{
            //    Duration = Timing.half;
            //}
            //if (NoteInfo.IndexOf('t') != -1)
            //{
            //    Duration = Timing.third;
            //}
            //if (NoteInfo.IndexOf('w') != -1)
            //{
            //    Duration = Timing.whole;
            //}
            #endregion
        }


        public void FillChromatic(string NoteInfo)
        {
            if (NoteInfo.IndexOf('b') != -1)
            {
                Chromatic = (int)ChromaticEnum.Flat;
            }
            else if (NoteInfo.IndexOf('#') != -1)
            {
                Chromatic = (int)ChromaticEnum.Sharp;
            }
            else if (NoteInfo.IndexOf('n') != -1)
            {
                Chromatic = (int)ChromaticEnum.Natural;
            }
            else
            {
                Chromatic = (int)ChromaticEnum.Natural;
            }
            #region old code
            //if (NoteInfo.IndexOf('f') != -1)
            //{
            //    this.TheChromatic = Chromatic.Flat;
            //}
            //if (NoteInfo.IndexOf('n') != -1)
            //{
            //    this.TheChromatic = Chromatic.Natural;
            //}
            //if (NoteInfo.IndexOf('s') != -1)
            //{
            //    this.TheChromatic = Chromatic.Sharp;
            //}
            #endregion
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
