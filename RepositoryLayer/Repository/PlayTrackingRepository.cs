using DataLayer.DbContext;
using DataLayer.DbObject;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class PlayTrackingRepository : GenericRepository<PlayTracking>, IPlayTrackingRepository
    {
        public PlayTrackingRepository(PianoContext dbContext) : base(dbContext)
        {
        }
    }
}
