using AutoMapper;
using DataLayer.DbObject;
using DataLayer.Migrations;
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
//        {
//  "songId": 2,
//  "instrumentId": 2,
//  "measures": [
//    {
//      "sheetId": 0,
//      "position": 1,
//      "chords": [
//        {
//          "noteIds": [
//            22, 24
//          ],
//          "duration": 4,
//          "chromatic": 1,
//          "measureId": 0,
//          "position": 1
//        }
//      ]
//    }
//  ]
//}

        private void MapChord()
        {
            CreateMap<Chord, ChordGetDto>();
            CreateMap<ChordCreateDto, Chord>();
                //.ForMember(dest=>dest.ChordNotes, opt =>
                //{
                //    opt.MapFrom(src => src.NoteIds.Select(noteId => new ChordNote { NoteId = noteId }));
                //});
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
            CreateMap<SongCreateDto, Song>()
                .ForMember(dest => dest.Sheets, opt =>
                {
                    opt.MapFrom(src => new List<SheetCreateDto> { src.Sheet });
                });
            CreateMap<SongSymbolCreateDto, Song>()
                .ForMember(dest => dest.Sheets, opt =>
                {

                    opt.MapFrom(src =>
                         new List<Sheet> 
                         { new Sheet(src.Sheet.SongId, src.Sheet.InstrumentId, src.Sheet.TopSignature, src.Sheet.BottomSignature, src.Sheet.RightSymbol, src.Sheet.LeftSymbol) }
                    );
                });
        }
    }
}
