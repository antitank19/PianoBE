using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DbContext;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;

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

        // GET api/<SheetsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SheetsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
