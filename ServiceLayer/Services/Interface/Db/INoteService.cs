using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOs;

namespace ServiceLayer.Services.Interface.Db
{
    public interface INoteService
    {
        /*
        public IQueryable<T> GetNoteList<T>(); 
        */
        Task<IEnumerable<NoteGetDto>> GetNoteList<T>();
        public Task<T> GetNoteById<T>(int id);
        public Task<bool> IsIdExisted<T>(int id);
    }
}
