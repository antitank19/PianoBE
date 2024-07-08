using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs.Sheet
{
    public class SheetSymbolCreateDto
    {
        public int SongId { get; set; }
        public int InstrumentId { get; set; }
        public int TopSignature { get; set; }
        public int BottomSignature { get; set; }
        public string Symbols { get; set; }
    }
}
