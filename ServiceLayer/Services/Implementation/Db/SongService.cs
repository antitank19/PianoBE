using DataLayer.DbContext;
using ServiceLayer.Services.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation.Db
{
    public class SongService : ISongService
    {
        private readonly PianoContext context;

        public SongService(PianoContext context)
        {
            this.context = context;
        }

        public Task<T> GetSongById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetSongList<T>()
        {
            throw new NotImplementedException();
        }
    }
}
