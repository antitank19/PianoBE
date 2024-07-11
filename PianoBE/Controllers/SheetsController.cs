using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheetsController : ControllerBase
    {
        private readonly IServiceWrapper services;
        
        public SheetsController(IServiceWrapper services)
        {
            this.services = services;
        }
        // GET: api/<SheetsController>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            IQueryable<SheetGetDto> dtos = services.Sheets.GetSheetList<SheetGetDto>();
            return Ok(dtos);
        }

        [HttpGet("Song/{songId}")]
        public async Task<IActionResult> GetListBySong(int songId)
        {
            if (!await services.Songs.IsExistAsync(songId))
            {
                return NotFound("Không tìm thấy bài hát");
            }

            IQueryable<SheetGetDto> dtos = services.Sheets.GetSheetListBySongId<SheetGetDto>(songId);
            return Ok(dtos);
        }

        // GET api/<SheetsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            SheetGetDto dto = await services.Sheets.GetSheetByIdAsync<SheetGetDto>(id);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSheet(SheetCreateDto input)
        {
            var created = await services.Sheets.CreateSheetAsync(input);
            return Ok(created);
        }

        [HttpPost("symbol")]
        public async Task<IActionResult> CreateSheetWithSymbol(SheetSymbolCreateDto input)
        {
            var created = await services.Sheets.CreateSheetAsync(input);
            return Ok(created);
        }

        [HttpPost("Midi")]
        public async Task<IActionResult> CreateSheetWithMidi([FromForm] SheetMidiCreateDto input)
        {
            var created = await services.Sheets.CreateSheetAsync(input);
            return Ok(created);
        }

        // PUT api/<SheetsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SheetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
