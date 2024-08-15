using AutoMapper;
using DataLayer.DbObject;
using ServiceLayer.DTOs;
using ServiceLayer.DTOs.User;
using ServiceLayer.ModelViews.Genre;
using ServiceLayer.ModelViews.Instruments;
using ServiceLayer.ModelViews.Songs;

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
            MapUser();
            MapGenre();
            MapInstruments();
        }

        private void MapInstruments()
        {
            CreateMap<Instrument, UpdateInstrumentsModel>().ReverseMap();
            CreateMap<Instrument, CreateInstrumentsModel>().ReverseMap();
            CreateMap<Instrument, ResponseInstrumentsModel>().ReverseMap();
        }


        private void MapGenre()
        {
            CreateMap<Genre, GetGenreResponse>().ReverseMap();
            CreateMap<Genre, CreateGenreResponse>().ReverseMap();
            CreateMap<CreateGenreRequest, Genre>();
            CreateMap<UpdateGenreRequest, Genre>();
            CreateMap<UpdateGenreRequest, UpdateGenreResponse>();
            CreateMap<Genre, UpdateGenreResponse>().ReverseMap();
        }

        private void MapNote()
        {
            CreateMap<Note, NoteGetDto>();
        }

        private void MapUser()
        {
            CreateMap<User, GetArtistInSongResponse>().ReverseMap();

            CreateMap<AddNewUserDto, User>()
                .ForMember(dest => dest.DateOfBirth, opt =>
                {
                    opt.MapFrom(src => DateOnly.Parse(src.DateOfBirth));
                });
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.DateOfBirth, opt =>
                {
                    opt.MapFrom(src => DateOnly.Parse(src.DateOfBirth));
                });
        }

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
                         new List<Sheet> { 
                             new Sheet(src.Sheet.SongId, src.Sheet.InstrumentId, src.Sheet.TopSignature, src.Sheet.BottomSignature, src.Sheet.KeySignature, src.Sheet.RightSymbol, src.Sheet.LeftSymbol) 
                         }
                    );
                });
            CreateMap<Song, SongResponse>()
               .ForMember(dest => dest.Genres, opt =>
               {
                   opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList());
               });
        }
    }
}
