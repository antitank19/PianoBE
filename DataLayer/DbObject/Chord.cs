using DataLayer.EnumsAndConsts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataLayer.DbObject
{
    public class Chord
    {
        public Chord() { }
        /// <summary>
        /// Create a music sheet using string
        /// </summary>
        /// <param name="measureId"></param>
        /// <param name="chordString">
        /// (Note)(Note)(Note)(Octave)(Chromatic)_(Duration)
        ///     Note: C D E F G A B = do re mi fa sol la si, - for pause
        ///     Octave: 1-8 with 4 is the ussual
        ///     Chormatic: # for sharp, b for flat
        /// </param>
        public Chord(int measureId, int position, string chordString)
        {
            try
            {
                MeasureId = measureId;
                Position = position;

                string[] noteSymbols = new string[] { PitchConst.C, PitchConst.D, PitchConst.E, PitchConst.F, PitchConst.G, PitchConst.A, PitchConst.B, PitchConst.Pause };
                string regex = @"(?=[" + PitchConst.C + PitchConst.D + PitchConst.E + PitchConst.F + PitchConst.G + PitchConst.A + PitchConst.B + PitchConst.Pause + @"])";
                //string[] noteStrings = chordString.Split(noteSymbols, StringSplitOptions.RemoveEmptyEntries);
                var noteStrings = Regex.Split(chordString, regex).Where(s => !String.IsNullOrWhiteSpace(s));
                ChordNotes = noteStrings.Select(noteString => new ChordNote(noteString)).ToList();
                //FillPitch(chordString);
                //FillOctave(chordString);

                FillDuration(chordString);
            }
            catch (Exception ex)
            {
                if (ex is WrongNoteStringFormatException) throw ex;
            }
        }
        public int Id { get; set; }
        public float Duration { get; set; }
        // Flat:0, Thường:1, Sharp: 2

        //Position: thứ tự note trong khuôn nhạc
        public int Position { get; set; } = 1;
        public ICollection<ChordNote> ChordNotes { get; set; }

        //Measure: thứ tự khuôn nhạc
        public int MeasureId { get; set; }
        public Measure Measure { get; set; }



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



    }
}
