using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepository;

namespace RepositoryLayer.Repository
{
    public class InstrumentRepository : GenericRepository<Instrument>, IInstrumentRepository
    {
        protected readonly PianoContext _context;
        private readonly IMapper _mapper;

        public InstrumentRepository(PianoContext dbContext, IMapper mapper) : base(dbContext)
        {
            _context = dbContext;
            _mapper = mapper;
        }
        public IQueryable<T> GetList<T>()
        {
            return _context.Instruments.ProjectTo<T>(_mapper.ConfigurationProvider);
        }
        public async Task<bool> IsExistAsync(int id)
        {
            return await _context.Instruments.AnyAsync(x => x.Id == id);
        }
    }
}
