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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SongId { get; set; }
        public Song Song { get; set; }
        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }
        public string SheetFile { get; set; }
        public ICollection<SongNote> SongNotes { get; set; }
    }
}
