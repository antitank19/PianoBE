using AutoMapper;
using DataLayer.DbContext;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Services.Implementation.Db;
using ServiceLayer.Services.Interface;
using ServiceLayer.Services.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.IRepository;
using System.Net;

namespace ServiceLayer.Services.Implementation
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly PianoContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        private readonly IGenreService _genreService;
        private readonly ISystemService _systemService;
        private readonly ISongService _songService;
        private readonly ISheetService _sheetService;
        private readonly IInstrumentService _instrumentService;
        private readonly IArtistService _artistService;
        private readonly INoteService _noteService;
        private readonly IUserService _userService;
        private readonly IPlayTrackingService _plaTrackingService;
        public ServiceWrapper(PianoContext context, 
            IMapper mapper,
            IConfiguration config,
            IGenreService genreService,
            IInstrumentService instrumentService,
            ISystemService systemService,
            ISongService songService,
            ISheetService sheetService,
            IArtistService artistService,
            INoteService noteService,
            IUserService userService,
            IPlayTrackingService playTrackingService)
        {
            this.context = context;
            this.mapper = mapper;
            this.config = config;
            /*system = new SystemService(context);
            instruments = new InstrumentService(context, mapper);*/
            
            _instrumentService = instrumentService;
            _systemService = systemService;
            _songService = songService;
            _sheetService = sheetService;
            _genreService = genreService;
            _artistService = artistService;
            _noteService = noteService;
            _userService = userService;
            _plaTrackingService = playTrackingService;
        }
        public IGenreService GenreService => _genreService;
        public ISystemService SystemService => _systemService;
        public ISongService SongService => _songService;
        public ISheetService SheetService => _sheetService;
        public IInstrumentService InstrumentService => _instrumentService;
        public IArtistService ArtistService => _artistService;
        public INoteService NoteService => _noteService;

        public IUserService UserService => _userService;
        public IPlayTrackingService PlayTrackingService => _plaTrackingService;


        /*public ISystemService System
        {
            get
            {
                if (system == null)
                {
                    system = new SystemService(context);
                }
                return system;
            }
        }

        public ISongService Songs
        {
            get
            {
                if (songs == null)
                {
                    songs = new SongService(context, mapper, config, _unitOfWork);
                }
                return songs;
            }
        }

        public ISheetService Sheets
        {
            get
            {
                if (sheets == null)
                {
                    sheets = new SheetService(context, mapper, config);
                }
                return sheets;
            }
        }

        public IInstrumentService Instruments
        {
            get
            {
                if (instruments == null)
                {
                    instruments = new InstrumentService(context, mapper);
                }
                return instruments;
            }
        }*/

    }
}
