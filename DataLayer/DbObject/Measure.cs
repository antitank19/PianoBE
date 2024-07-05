using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbObject
{
    public class Measure
    {
        public Measure()
        {
            
        }
        public Measure(int sheetId, int position, string measureString)
        {
            SheetId = sheetId;
            Position = position;
            string[] noteStrings = measureString.Split(new char[] { ' ' });
            SongNotes = noteStrings.Select((nString, i)=> new SongNote(0,i+1, nString)).ToList();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SheetId { get; set; }
        public Sheet Sheet { get; set; }
        public int Position { get; set; }
        public ICollection<SongNote> SongNotes { get; set; }

    }
}
