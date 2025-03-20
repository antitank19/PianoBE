using API.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DTOs;
using ServiceLayer.ModelViews;
using ServiceLayer.ModelViews.Songs;
using ServiceLayer.Services.Implementation;
using ServiceLayer.Services.Interface;
using ServiceLayer.Utils;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> GetSongList(int pageNum = 1, int pageSize = 100)
        {
            if((await services.SongService.CountSongs())< (pageNum - 1) * pageSize)
            {
                return BadRequest("Not enough songs");
            }
            return Ok(services.SongService.GetSongList<SongGetDto>().Skip((pageNum-1)*pageSize).Take(pageSize));
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            SongGetDto dto = await services.SongService.GetSongById<SongGetDto>(id);
            return Ok(dto);
        }

        /// <summary>
        /// Tạo bài hát (không tạo kèm sheet
        /// Phải tạo sheet nhạc bằng api POST sheets/symbols
        /// </summary>
        /// <param name="input">
        /// Thông tin bài hát
        /// </param>
        /// <returns></returns>
        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> CreateSong([FromForm] SongCreateDto input)
        {
            SongGetDto dto = await services.SongService.CreateSong(input);
            return Ok(dto);
        }

        [HttpPost("Symbol")]
        public async Task<IActionResult> CreateSongWithSymbol([FromForm] SongSymbolCreateDto input)
        {
            SongGetDto dto = await services.SongService.CreateSong(input);
            return Ok(dto);
        }

        [HttpPost("Midi")]
        public async Task<IActionResult> CreateSongWithMidi([FromForm] SongMidiCreateDto input)
        {
            SongGetDto dto = await services.SongService.CreateSong(input);
            return Ok(dto);
        }

        /// <summary>
        /// Cập nhật bài hát 
        /// </summary>
        /// <param name="input">
        /// Thông tin bài hát
        /// </param>
        /// <returns></returns>
        // PUT api/<SongsController>
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] SongUpdateDto input)
        {
            SongGetDto dto = await services.SongService.UpdateSong(input);
            return Ok(dto);
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await services.SongService.DeleteSong(id);
            return Ok(new BaseResponse<string>("Delete Songs successfully", StatusCodes.Status200OK));
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllArtistSongAndPage([Required] int pageNum, [Required] int pageSize, string? keyword)
        {
            SongResponseByArtistPage pageResponse = await services.SongService.getSongsByArtistAndPage(pageNum, pageSize, keyword);
            return Ok(new BaseResponse<SongResponseByArtistPage>("Get Songs by page successfully!", StatusCodes.Status200OK, pageResponse));
        }

        [HttpGet("genre")]
        public async Task<IActionResult> getAllSongByGenreAndPage([Required] int pageNum, [Required] int pageSize, int? id, string? keyword)
        {
            SongResponseByGenrePage pageResponse = await services.SongService.getSongsByGenreAndPage(pageNum, pageSize, id, keyword);
            return Ok(new BaseResponse<SongResponseByGenrePage>("Get Songs by genre successfully!", StatusCodes.Status200OK, pageResponse));
        }

        [HttpGet("my-song")]
        public IActionResult GetSongByUsername()
        {
            string username = User.GetUsername();
            if (!string.IsNullOrWhiteSpace(username))
            {
                List<SongResponse> response = services.SongService.FindSongsByNameAsync(username);
                return Ok(new BaseResponse<List<SongResponse>>("Get Songs by Artist successfully!", StatusCodes.Status200OK, response));
            }
            return BadRequest();
        }

    }
}
