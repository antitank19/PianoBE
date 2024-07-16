﻿using System;
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
        public Sheet(int songId, int instrumentId, int topSignature, int bottomSignature, string sheetString, string? leftHandSheetString=null)
        {
            SongId = songId;
            InstrumentId = instrumentId;
            TopSignature = topSignature;
            BottomSignature = bottomSignature;
            string[] measureStrings = sheetString.Split('/');
            //var measures = measureStrings.Select(mString => new Measure(mString));
            //Measures = (ICollection<Measure>?)measureStrings.Select(mString => new Measure(mString));
            Measures = measureStrings.Select((mString, n) => new Measure(0, n + 1, mString)).ToList();
            if (!String.IsNullOrWhiteSpace(leftHandSheetString))
            {
                LeftHandSheet = new Sheet(songId, InstrumentId, topSignature, bottomSignature, leftHandSheetString);
            }
            //foreach
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SongId { get; set; }
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

        public int? LeftHandSheetId { get; set; }
        public Sheet? LeftHandSheet { get; set; }
        public ICollection<Measure> Measures { get; set; }
    }
}
