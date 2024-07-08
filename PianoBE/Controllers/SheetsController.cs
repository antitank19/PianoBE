using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using DataLayer.DbObject;
using ServiceLayer.DTOs.Sheet;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheetsController : ControllerBase
    {
        private readonly PianoContext context;
        private readonly IMapper mapper;
        public SheetsController(PianoContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        // GET: api/<SheetsController>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(context.Sheets.ProjectTo<SheetGetDto>(mapper.ConfigurationProvider));
        }

        [HttpGet("Song/{songId}")]
        public async Task<IActionResult> GetListBySong(int songId)
        {
            if (!await context.Songs.AnyAsync(s=>s.Id==songId)) 
            {
                return NotFound("Không tìm thấy bài hát");
            }
            return Ok(context.Sheets.Where(s=>s.SongId==songId).ProjectTo<SheetGetDto>(mapper.ConfigurationProvider));
        }

        // GET api/<SheetsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Sheet sheet = await context.Sheets
                .Include(s => s.Measures).ThenInclude(s => s.Chords).ThenInclude(s=>s.ChordNotes).ThenInclude(sn=>sn.Note)
                .FirstOrDefaultAsync(x => x.Id == id);
            SheetGetDto dto = mapper.Map<SheetGetDto>(sheet);    
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSheet(SheetCreateDto input)
        {
            Sheet sheet = mapper.Map<Sheet>(input);
            await context.Sheets.AddAsync(sheet);
            await context.SaveChangesAsync();
            SheetGetDto dto = mapper.Map<SheetGetDto>(sheet);
            return Ok(dto);
        }

        [HttpPost("symbol")]
        public async Task<IActionResult> CreateSheetWithSymbol(SheetSymbolCreateDto input)
        {
            Sheet sheet = new Sheet(input.SongId, input.InstrumentId, input.Symbols);
            await context.Sheets.AddAsync(sheet);
            await context.SaveChangesAsync();
            SheetGetDto dto = mapper.Map<SheetGetDto>(sheet);
            return Ok(dto);
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
