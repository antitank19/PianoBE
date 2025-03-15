using DataLayer.DbContext;
using DataLayer.DbObject;
using RepositoryLayer.IRepository;

namespace RepositoryLayer.Repository
{
    public class SheetRepository : GenericRepository<Sheet>, ISheetRepository
    {
        public SheetRepository(PianoContext dbContext) : base(dbContext)
        {
        }
    }
}
