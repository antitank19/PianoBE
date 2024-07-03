using DataLayer.DbContext;
using ServiceLayer.Services.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation.Db
{
    public class NoteService : INoteService
    {
        private readonly PianoContext context;

        public NoteService(PianoContext context)
        {
            this.context = context;
        }

        public Task<T> GetNoteById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetNoteList<T>()
        {
            throw new NotImplementedException();
        }
    }
}
