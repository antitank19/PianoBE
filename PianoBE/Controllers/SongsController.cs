using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly PianoContext context;
        private readonly IMapper mapper;
        public SongsController(PianoContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
            Song song = await context.Songs.SingleOrDefaultAsync(s => s.Id == id);
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
