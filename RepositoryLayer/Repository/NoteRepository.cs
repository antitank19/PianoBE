using DataLayer.DbContext;
using DataLayer.DbObject;
using RepositoryLayer.IRepository;

namespace RepositoryLayer.Repository
{
    public class NoteRepository : GenericRepository<Note>, INoteRepository
    {
        public NoteRepository(PianoContext dbContext) : base(dbContext)
        {
        }
    }
}
