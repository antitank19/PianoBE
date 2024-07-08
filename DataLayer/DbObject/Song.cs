using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbObject
{
    /// <summary>
    /// Tổng = Sum( listnode.Select(x=>x.durration) )
    /// let displayedNotes = [];
    /// let currentX   =0
    /// for(i=0;i<listnode.legnth;i++){
    ///     newNote.x=Tổng - curentX*khoảng cách 2 note đen(1);
    ///     displayedNotes.add( newNote)
    ///     currentX -= newNote.durration
    /// }
    /// </summary>
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Composer { get; set; }
        public string Genre { get; set; }
        public int ArtistId { get; set; }
        public User Artist { get; set; }
        /// <summary>
        /// Signature là cái kí hiệu cho như 2/4, 3/4 trên khuôn nhạc
        /// </summary>
        public int Signature1 { get; set; }
        public int Signature2 { get; set; }
        public ICollection<Sheet> Sheets { get; set; } = new List<Sheet>();
    }
}
  