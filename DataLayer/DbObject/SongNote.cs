using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbObject
{
    public class SongNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int NoteID { get; set; }
        public Note Note { get; set; }
        public int SheetID { get; set; }
        public Sheet Sheet { get; set; }
        public float Duration { get; set; }
        // Flat:0, Thường:1, Sharp: 2
        public int Chromatic { get; set; }
        //Vị trí nốt nhạc
        //Measure: thứ tự khuôn nhạc
        //Position: thứ tự note trong khuôn nhạc
        public int Measure { get; set; }
        public int Position { get; set; }
    }
}
