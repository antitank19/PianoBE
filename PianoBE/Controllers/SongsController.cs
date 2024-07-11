using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DTOs;
using ServiceLayer.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly PianoContext context;
        private readonly IMapper mapper;
        public SongsController(PianoContext context, IMapper mapper, IConfiguration config)
        {
            this.context = context;
            this.mapper = mapper;
            this.config = config;
        }
        // GET: api/<SongsController>
        [HttpGet]
        public async Task<IActionResult> GetSongList()
        {
            return Ok(context.Songs.ProjectTo<SongGetDto>(mapper.ConfigurationProvider));
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Song song = await context.Songs
                .Include(s => s.Sheets).ThenInclude(s => s.Instrument)
                .Include(s => s.Sheets).ThenInclude(s => s.Measures).ThenInclude(m => m.Chords).ThenInclude(c => c.ChordNotes).ThenInclude(cn => cn.Note)
                .AsSingleQuery()
                .SingleOrDefaultAsync(s => s.Id == id);
            SongGetDto dto = mapper.Map<SongGetDto>(song);
            return Ok(dto);
        }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> CreateSong([FromBody] SongCreateDto input)
        {
            Song newSong = mapper.Map<Song>(input);
            await context.Songs.AddAsync(newSong);
            await context.SaveChangesAsync();
            SongGetDto dto = mapper.Map<SongGetDto>(newSong);
            return Ok(dto);
        }

        [HttpPost("Symbol")]
        public async Task<IActionResult> CreateSongWithSymbol([FromBody] SongSymbolCreateDto input)
        {
            Song newSong = mapper.Map<Song>(input);
            await context.Songs.AddAsync(newSong);
            await context.SaveChangesAsync();
            SongGetDto dto = mapper.Map<SongGetDto>(newSong);
            return Ok(dto);
        }

        [HttpPost("Midi")]
        public async Task<IActionResult> CreateSongWithMidi([FromForm] SongMidiCreateDto input)
        {
            string midiUrl = await FirebaseStorageUtil.UploadFileAsync(input.Sheet.SheetFile, "Midi", config["Firebase:StorageBucket"]);
            Song newSong = new Song
            {
                ArtistId = input.ArtistId,
                Genre = input.Genre,
                Composer = input.Composer,
                Title = input.Title,
                Sheets = new Sheet[]
                {
                    new Sheet
                    {
                        BottomSignature = input.Sheet.BottomSignature,
                        TopSignature = input.Sheet.TopSignature,
                        InstrumentId = input.Sheet.InstrumentId,
                        SheetFile = midiUrl,
                    }
                },
            };
            await context.Songs.AddAsync(newSong);
            await context.SaveChangesAsync();
            SongGetDto dto = mapper.Map<SongGetDto>(newSong);
            return Ok(dto);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
