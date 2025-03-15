using DataLayer.EnumsAndConsts;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.DTOs
{
    public class SheetXmlCreateDto
    {
        public int SongId { get; set; }
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public KeySignatureEnum KeySignature { get; set; }
        public int TopSignature { get; set; }
        public int BottomSignature { get; set; }
        public string RightSymbol { get; set; }
        public string? LeftSymbol { get; set; }
        public IFormFile XmlFile { get; set; }
        public IFormFile? BackgroundMusic { get; set; }
    }
}
