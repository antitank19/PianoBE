using AutoMapper;
using DataLayer.DbObject;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapper
{
    public class MyMapperProfile : Profile
    {
        //CreateMap<src, dest>
        public MyMapperProfile()
        {
            MapSong();
            MapSongNote();
            MapSheet();
            
        }

        private void MapSongNote()
        {
            CreateMap<SongNote, SongNoteGetDto>();
        }

        private void MapSheet()
        {
            CreateMap<Sheet, SheetGetDto>();
        }

        private void MapSong() { 
            CreateMap<Song, SongGetDto>();
        }
    }
}
