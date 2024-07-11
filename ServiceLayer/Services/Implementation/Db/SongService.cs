using AutoMapper;
using DataLayer.DbContext;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper mapper;

        public SongService(PianoContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<T> GetSongById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetSongList<T>()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExistAsync(int songId)
        {
           return await context.Songs.AnyAsync(s => s.Id == songId);
        }
    }
}
