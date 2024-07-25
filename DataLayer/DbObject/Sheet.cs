using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbObject
{
    public class Sheet
    {
        public Sheet()
        {

        }
        public Sheet(int songId, int instrumentId, int topSignature, int bottomSignature, string rightSheetString, string? leftSheetString = null)
        {
            SongId = songId;
            InstrumentId = instrumentId;
            TopSignature = topSignature;
            BottomSignature = bottomSignature;
            string[] measureStrings = rightSheetString.Split('/');
            //var measures = measureStrings.Select(mString => new Measure(mString));
            //Measures = (ICollection<Measure>?)measureStrings.Select(mString => new Measure(mString));
            RightSymbol = rightSheetString;
            RightMeasures = measureStrings.Select((mString, n) => new Measure(0, n + 1, mString, true)).ToList();
            if (!String.IsNullOrWhiteSpace(leftSheetString))
            {
                //LeftHandSheet = new Sheet(songId, InstrumentId, topSignature, bottomSignature, leftSheetString);
                LeftMeasures = measureStrings.Select((mString, n) => new Measure(0, n + 1, mString, false)).ToList();
                LeftSymbol = leftSheetString;
            }
            //foreach
        }
        public void ToSymbol(List<Note> noteLists)
        {
            StringBuilder rightSB1 = new StringBuilder("");
            foreach (var measure in RightMeasures)
            {
                string measureString = measure.ToSymbol(noteLists);
                rightSB1.Append(measureString);
            }
            rightSB1.Remove(rightSB1.Length - 1, 1);
            RightSymbol = rightSB1.ToString();
            if (LeftMeasures.Count != 0)
            {
                StringBuilder leftSB = new StringBuilder("");
                foreach (var measure in RightMeasures)
                {
                    string measureString = measure.ToSymbol(noteLists);
                    leftSB.Append(measureString);
                }
                leftSB.Remove(leftSB.Length - 1, 1);
                LeftSymbol = leftSB.ToString();
            }

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SongId { get; set; }
        public int Difficulty { get; set; }
        ///// <summary>
        ///// Signature là cái kí hiệu cho như 2/4, 3/4 trên khuôn nhạc
        ///// </summary>
        public int TopSignature { get; set; }
        public int BottomSignature { get; set; }
        public Song Song { get; set; }
        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }
        /// <summary>
        /// Link of .mid file
        /// </summary>
        public string? SheetFile { get; set; }

        public string? RightSymbol { get; set; }
        public ICollection<Measure> RightMeasures { get; set; }   = new List<Measure>();
        public string? LeftSymbol { get; set; }
        public ICollection<Measure>? LeftMeasures { get; set; }  = new List<Measure> ();
    }
}
