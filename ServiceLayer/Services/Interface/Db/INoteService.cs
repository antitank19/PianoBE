using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface.Db
{
    public interface INoteService
    {
        public IQueryable<T> GetNoteList<T>(); 
        public Task<T> GetNoteById<T>(int id);
        public Task<bool> IsIdExisted<T>(int id);
    }
}
