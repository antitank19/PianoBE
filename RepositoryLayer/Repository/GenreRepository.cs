using DataLayer.DbContext;
using DataLayer.DbObject;
using RepositoryLayer.IRepository;

namespace RepositoryLayer.Repository;

public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    private readonly PianoContext _context;
    public GenreRepository(PianoContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }
}