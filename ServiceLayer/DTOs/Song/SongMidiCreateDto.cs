using Microsoft.AspNetCore.Http;

namespace ServiceLayer.DTOs
{
    public class SongMidiCreateDto
    {
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public string Composer { get; set; }
        public SheetMidiCreateDto Sheet { get; set; }

    }
}
