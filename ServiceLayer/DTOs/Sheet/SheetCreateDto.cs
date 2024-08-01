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
        public int TopSignature { get; set; }
        public int BottomSignature { get; set; }
        public ICollection<MeasureCreateDto> RightMeasures { get; set; }
        public ICollection<MeasureCreateDto> LeftMeasures { get; set; }
    }
}
