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
    public class Measure
    {
        public Measure()
        {

        }
        public Measure(int sheetId, int position, string measureString, bool isRightHand)
        {
            if (isRightHand)
            {
                RightSheetId = sheetId;
            }
            else
            {
                LeftSheetId = sheetId;
            }
            Position = position;
            string[] chordStrings = measureString.Split(new char[] { ' ' });
            //if (chordStrings[0].Length == 1)
            //{
            //    if (chordStrings[0].StartsWith('F'))
            //    {
            //        Clef = (int)ClefEnum.Fa;
            //    }
            //    else
            //    {
            //        Clef = (int)ClefEnum.Sol;
            //    }

            //    IEnumerable<string> realChordStrings = chordStrings.Skip(1);
            //    Chords = realChordStrings.Select((nString, i) => new Chord(0, i + 1, nString)).ToList();
            //}
            //else
            //{
            //    Clef = (int)ClefEnum.Sol;
            //}
            Chords = chordStrings.Select((nString, i) => new Chord(0, i + 1, nString)).ToList();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? RightSheetId { get; set; }
        public Sheet? RightSheet { get; set; }
        public int? LeftSheetId { get; set; }
        public Sheet? LeftSheet { get; set; }

        public int Position { get; set; }
        public ICollection<Chord> Chords { get; set; }
        public string ToSymbol(List<Note> noteList)
        {
            var sb = new StringBuilder();
            //if (Clef == (int)ClefEnum.Sol)
            //{
            //    sb.Append("F ");
            //}
            foreach (Chord ch in Chords)
            {
                sb.Append(ch.ToSymbol(noteList));
            }
            string measureString = sb.ToString().Trim()+"/";
            return measureString;
        }

    }
}
