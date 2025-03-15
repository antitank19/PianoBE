using AutoMapper.QueryableExtensions;
using AutoMapper;
using DataLayer.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.ModelViews;
using ServiceLayer.ModelViews.Enums;
using ServiceLayer.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly PianoContext context;
        private readonly IMapper mapper;
        private readonly IServiceWrapper _serviceWrapper;
        public NotesController(PianoContext context, IMapper mapper,
            IServiceWrapper serviceWrapper)
        {
            this.context = context;
            this.mapper = mapper;
            _serviceWrapper = serviceWrapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetList()
        {
            /*
            return Ok(context.Notes.Where(x=>x.IsActive == true && x.IsDeleted == false).ProjectTo<NoteGetDto>(mapper.ConfigurationProvider));
        */
            var note = await _serviceWrapper.NoteService.GetNoteList<NoteGetDto>();
            return Ok(new BaseResponse<IEnumerable<NoteGetDto>>("Get all Notes successfully", StatusCodes.Status200OK, note));
        }
    }
}
