using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface.Db
{
    public interface IInstrumentService
    {
        public IQueryable<T> GetList<T>();
        public Task<bool> IsExistAsync(int id);
    }
}
