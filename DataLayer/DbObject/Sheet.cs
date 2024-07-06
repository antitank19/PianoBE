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
        public Sheet(int songId, int instrumentId, string sheetString)
        {
           
                SongId = songId;
                InstrumentId = instrumentId;
                string[] measureStrings = sheetString.Split('/');
                //var measures = measureStrings.Select(mString => new Measure(mString));
                //Measures = (ICollection<Measure>?)measureStrings.Select(mString => new Measure(mString));
                Measures = measureStrings.Select((mString, n) => new Measure(0, n + 1, mString)).ToList();
                //foreach
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SongId { get; set; }
        public Song Song { get; set; }
        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }
        /// <summary>
        /// Link of .mid file
        /// </summary>
        public string? SheetFile { get; set; }
        public ICollection<Measure> Measures { get; set; }
    }
}
