using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DTOs;
using ServiceLayer.Services.Interface;
using ServiceLayer.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly IServiceWrapper services;
        public SongsController(IServiceWrapper services)
        {
            this.services = services;
        }
        // GET: api/<SongsController>
        [HttpGet]
        public async Task<IActionResult> GetSongList()
        {
            return Ok(services.Songs.GetSongList<SongGetDto>());
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            SongGetDto dto = await services.Songs.GetSongById<SongGetDto>(id);
            return Ok(dto);
        }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> CreateSong([FromBody] SongCreateDto input)
        {
            SongGetDto dto = await services.Songs.CreateSong(input);
            return Ok(dto);
        }

        [HttpPost("Symbol")]
        public async Task<IActionResult> CreateSongWithSymbol([FromBody] SongSymbolCreateDto input)
        {
            SongGetDto dto = await services.Songs.CreateSong(input);
            return Ok(dto);
        }

        [HttpPost("Midi")]
        public async Task<IActionResult> CreateSongWithMidi([FromForm] SongMidiCreateDto input)
        {
            SongGetDto dto = await services.Songs.CreateSong(input);
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
