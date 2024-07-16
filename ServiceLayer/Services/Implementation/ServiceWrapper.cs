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

namespace ServiceLayer.Services.Implementation
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly PianoContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public ServiceWrapper(PianoContext context, IMapper mapper, IConfiguration config)
        {
            this.context = context;
            this.mapper = mapper;
            this.config = config;
            system = new SystemService(context);
            instruments = new InstrumentService(context, mapper);
        }

        private ISystemService system;
        public ISystemService System
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

        private ISongService songs;
        public ISongService Songs
        {
            get
            {
                if (songs == null)
                {
                    songs = new SongService(context, mapper, config);
                }
                return songs;
            }
        }

        private ISheetService sheets;
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

        private IInstrumentService instruments;
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
        }
    }
}
