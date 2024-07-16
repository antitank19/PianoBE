using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation.Db
{
    public class InstrumentService : IInstrumentService
    {
        private readonly PianoContext context;
        private readonly IMapper mapper;

        public InstrumentService(PianoContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IQueryable<T> GetList<T>()
        {
            return context.Instruments.ProjectTo<T>(mapper.ConfigurationProvider);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await context.Instruments.AnyAsync(x => x.Id == id);
        }
    }
}
