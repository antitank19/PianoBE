using AutoMapper;
using DataLayer.DbObject;
using ServiceLayer.DTOs;
using ServiceLayer.DTOs.Note;
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
            MapSheet();
            MapMeasure();
            MapChord();
            MapChordNote();
            MapNote();
        }

        private void MapNote()
        {
            CreateMap<Note, NoteGetDto>();
        }

        private void MapChord()
        {
            CreateMap<Chord, ChordGetDto>();
        }

        private void MapChordNote()
        {
            CreateMap<ChordNote, ChordNoteGetDto>();

            CreateMap<ChordNoteCreateDto, ChordNote>();
        }

        private void MapMeasure()
        {
            CreateMap<Measure, MeasureGetDto>();

            CreateMap<MeasureCreateDto, Measure>();
        }

        private void MapSheet()
        {
            CreateMap<Sheet, SheetGetDto>();

            CreateMap<SheetCreateDto, Sheet>();
        }

        private void MapSong() { 
            CreateMap<Song, SongGetDto>();
        }
    }
}
