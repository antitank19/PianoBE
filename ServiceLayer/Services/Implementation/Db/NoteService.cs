using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper mapper;

        public NoteService(PianoContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<T> GetNoteById<T>(int id)
        {
            Note note = await context.Notes.SingleOrDefaultAsync(x => x.Id == id);
            return mapper.Map<T>(note);
        }

        public IQueryable<T> GetNoteList<T>()
        {
            return context.Notes.ProjectTo<T>(mapper.ConfigurationProvider);
        }

        public async Task<bool> IsIdExisted<T>(int id)
        {
            return await context.Notes.AnyAsync(x => x.Id == id);
        }
    }
}
