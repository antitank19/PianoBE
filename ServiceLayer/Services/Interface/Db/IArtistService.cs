using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface.Db
{
    public interface IArtistService
    {
        public IQueryable<T> GetArtistList<T>();
        public Task<T> GetArtistById<T>();
    }
}
