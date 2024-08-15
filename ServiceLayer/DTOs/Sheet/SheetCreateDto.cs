using DataLayer.EnumsAndConsts;

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
        public KeySignatureEnum KeySignature { get; set; }
    }
}
