using DataLayer.DbContext;
using ServiceLayer.Services.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation.Db
{
    public class SheetService : ISheetService
    {
        private readonly PianoContext context;

        public SheetService(PianoContext context)
        {
            this.context = context;
        }

        public Task<T> GetSheetById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetSheetList<T>()
        {
            throw new NotImplementedException();
        }
    }
}
