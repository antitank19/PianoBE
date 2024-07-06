using AutoMapper.QueryableExtensions;
using AutoMapper;
using DataLayer.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly PianoContext context;
        private readonly IMapper mapper;

        public NotesController(PianoContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(context.Notes.ProjectTo<NoteGetDto>(mapper.ConfigurationProvider));
        }
    }
}
