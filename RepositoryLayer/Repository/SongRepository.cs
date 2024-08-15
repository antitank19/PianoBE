using DataLayer.DbContext;
using DataLayer.DbObject;
using RepositoryLayer.IRepository;

namespace RepositoryLayer.Repository
{
    public class SongRepository : GenericRepository<Song>, ISongRepository
    {
        public SongRepository(PianoContext dbContext) : base(dbContext)
        {
        }
    }
}
