using DataLayer.DbObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SheetGetDto
    {
        public int Id { get; set; }

        public int SongId { get; set; }
        public string SongTitle { get; set; }
        public int TopSignature { get; set; }
        public int BottomSignature { get; set; }
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public string SheetFile { get; set; }
        public string RightSymbol { get; set; }
        public string LeftSymbol { get; set; }
        public ICollection<MeasureGetDto> RightMeasures { get; set; }
        public ICollection<MeasureGetDto> LeftMeasures { get; set; }
    }
}
