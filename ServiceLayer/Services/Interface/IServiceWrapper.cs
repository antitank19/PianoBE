using ServiceLayer.Services.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface
{
    public interface IServiceWrapper
    {
        IGenreService GenreService { get; }
        ISystemService SystemService { get; }
        ISongService SongService { get; }
        ISheetService SheetService { get; }
        IInstrumentService InstrumentService { get; }
        IArtistService ArtistService { get; }
        INoteService NoteService { get; }
        IUserService UserService { get; }
        IPlayTrackingService PlayTrackingService { get; }
        
        /*ISongService Songs { get; }
        ISheetService Sheets { get; }
        IInstrumentService Instruments { get; }
        ISystemService System { get; }
        IGenreService GenreService { get; }*/

    }
}
