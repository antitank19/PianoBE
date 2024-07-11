using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface.Db
{
    public interface ISongService
    {
        public IQueryable<T> GetSongList<T>();
        public Task<T> GetSongById<T>(int id);
        public Task<bool> IsExistAsync(int songId);
    }
}
