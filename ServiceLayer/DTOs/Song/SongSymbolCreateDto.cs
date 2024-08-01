
namespace ServiceLayer.DTOs
{
    public class SongSymbolCreateDto
    {
        public string Title { get; set; }
        public string Composer { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public SheetSymbolCreateDto Sheet { get; set; }
    }
}
