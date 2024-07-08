using ServiceLayer.DTOs.Sheet;

namespace ServiceLayer.DTOs
{
    public class SongSymbolCreateDto
    {
        public string Title { get; set; }
        public string Composer { get; set; }
        public string Genre { get; set; }
        public int ArtistId { get; set; }
        public ICollection<SheetSymbolCreateDto> Sheets { get; set; }
    }
}
