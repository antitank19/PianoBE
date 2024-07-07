using DataLayer.DbObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SheetCreateDto
    {
        public int SongId { get; set; }
        public int InstrumentId { get; set; }
        public ICollection<MeasureCreateDto> Measures { get; set; }
    }
}
