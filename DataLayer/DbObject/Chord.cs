using DataLayer.EnumsAndConsts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if (chordString.Contains(PitchConst.Pause))
                {

                    Chromatic = (int)ChromaticEnum.Natural;
                }
                else
                {
                    string[] noteSymbols = new string[] { PitchConst.C, PitchConst.D, PitchConst.E, PitchConst.F, PitchConst.G, PitchConst.A, PitchConst.B, PitchConst.Pause };
                    string[] noteStrings = chordString.Split(noteSymbols, StringSplitOptions.RemoveEmptyEntries);
                    ChordNotes = noteStrings.Select(noteString=>new ChordNote(noteString)).ToList(); 
                    //FillPitch(chordString);
                    //FillOctave(chordString);
                    FillChromatic(chordString);
                }
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
        public int Position { get; set; }  = 1;
        public int Chromatic { get; set; } = (int)ChromaticEnum.Natural;
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
}
