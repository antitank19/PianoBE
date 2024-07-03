using DataLayer.DbContext;
using ServiceLayer.Services.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation.Db
{
    public class ArtistService : IArtistService
    {
        private readonly PianoContext context;

        public ArtistService(PianoContext context)
        {
            this.context = context;
        }

        public Task<T> GetArtistById<T>()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetArtistList<T>()
        {
            throw new NotImplementedException();
        }
    }
}
