using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Base;
using DataLayer.EnumsAndConsts;

namespace DataLayer.DbObject
{
    public class Sheet: BaseEntity
    {
        public Sheet()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="songId">Id of sheet's song</param>
        /// <param name="instrumentId">Id of sheet's instrument</param>
        /// <param name="name">Name of sheet</param>
        /// <param name="level">Difficulty of sheet</param>
        /// <param name="topSignature"></param>
        /// <param name="bottomSignature"></param>
        /// <param name="keySignature"></param>
        /// <param name="rightSheetString"></param>
        /// <param name="leftSheetString"></param>
        public Sheet(int songId, int instrumentId, 
            string name, int level, int topSignature, int bottomSignature, 
            KeySignatureEnum keySignature, string rightSheetString, string? leftSheetString = null)
        {
            SongId = songId;
            InstrumentId = instrumentId;
            Name = name;
            Level = level;
            TopSignature = topSignature;
            BottomSignature = bottomSignature;
            KeySignature = keySignature;
            RightSymbol = rightSheetString;
            LeftSymbol = leftSheetString;
            //DecodeSymbolToMeasure();
            //foreach
        }

        //public void DecodeSymbolToMeasure()
        //{
        //    string[] rightMeasureStrings = RightSymbol.Split('/');
        //    //var measures = measureStrings.Select(mString => new Measure(mString));
        //    //Measures = (ICollection<Measure>?)measureStrings.Select(mString => new Measure(mString));
        //    RightMeasures = rightMeasureStrings.Select((mString, n) => new Measure(0, n + 1, mString, true)).ToList();
        //    if (!String.IsNullOrWhiteSpace(LeftSymbol))
        //    {
        //        string[] leftMeasureStrings = LeftSymbol.Split('/');
        //        //LeftHandSheet = new Sheet(songId, InstrumentId, topSignature, bottomSignature, leftSheetString);
        //        LeftMeasures = leftMeasureStrings.Select((mString, n) => new Measure(0, n + 1, mString, false)).ToList();
        //    }
        //}

        //public void ToSymbol(List<Note> noteLists)
        //{
        //    StringBuilder rightSB1 = new StringBuilder("");
        //    foreach (var measure in RightMeasures)
        //    {
        //        string measureString = measure.ToSymbol(noteLists);
        //        rightSB1.Append(measureString);
        //    }
        //    rightSB1.Remove(rightSB1.Length - 1, 1);
        //    RightSymbol = rightSB1.ToString();
        //    if (LeftMeasures.Count != 0)
        //    {
        //        StringBuilder leftSB = new StringBuilder("");
        //        foreach (var measure in LeftMeasures)
        //        {
        //            string measureString = measure.ToSymbol(noteLists);
        //            leftSB.Append(measureString);
        //        }
        //        leftSB.Remove(leftSB.Length - 1, 1);
        //        LeftSymbol = leftSB.ToString();
        //    }

        //}
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
        public int Level {  get; set; }
        public string Name {  get; set; }
        /// <summary>
        /// Link of .mid file
        /// </summary>
        public string? MidiFile { get; set; }
        public string? XmlFile { get; set; }
        public string? BackgroundMusicFile { get; set; }
        public KeySignatureEnum KeySignature { get; set; } = KeySignatureEnum.None;


        public string? RightSymbol { get; set; }
        //public ICollection<Measure> RightMeasures { get; set; }
        public string? LeftSymbol { get; set; }
        //public ICollection<Measure>? LeftMeasures { get; set; }
        public ICollection<PlayTracking> PlayTrackings { get; set; }
    }
}
