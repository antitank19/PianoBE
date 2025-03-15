using API.Extensions;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.DTOs.Tracking;
using ServiceLayer.ModelViews;
using ServiceLayer.Services.Implementation;
using ServiceLayer.Services.Interface;
using ServiceLayer.Validation;

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
        public async Task<IActionResult> GetList(int pageNumber = 1, int pageSize = 3)
        {
            IQueryable<SheetGetDto> dtos = services.SheetService.GetSheetList<SheetGetDto>();

            // Apply pagination
            dtos = dtos.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return Ok(dtos);
        }

        [HttpGet("Song/{songId}")]
        public async Task<IActionResult> GetListBySong(int songId)
        {
            if (!await services.SongService.IsExistAsync(songId))
            {
                return NotFound("Không tìm thấy bài hát");
            }

            IQueryable<SheetGetDto> dtos = services.SheetService.GetSheetListBySongId<SheetGetDto>(songId);
            return Ok(dtos);
        }

        // GET api/<SheetsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            SheetGetDto dto = await services.SheetService.GetSheetByIdAsync<SheetGetDto>(id);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSheet([FromForm]SheetCreateDto input)
        {
            ValidationResult valResult = new ValidationResult();
            try
            {
                await valResult.ValidateAsync(input, services);
                var created = await services.SheetService.CreateSheetAsync(input);
                if (!valResult.IsValid)
                {
                    return BadRequest(valResult);
                }
                return Ok(created);
            }
            catch (Exception ex)
            {
                valResult.AddError(ex.Message);
                return BadRequest(valResult);
            }
        }

        [HttpPost("symbol")]
        public async Task<IActionResult> CreateSheetWithSymbol([FromForm]SheetSymbolCreateDto input)
        {
            ValidationResult valRe = new ValidationResult();
            try
            {
                await valRe.ValidateAsync(input, services);
                if (!valRe.IsValid)
                {
                    return BadRequest(valRe);
                }
                SheetGetDto created = await services.SheetService.CreateSheetAsync(input);
                return Ok(created);
            }
            catch (Exception ex)
            {
                valRe.AddError(ex.Message);
                return BadRequest(valRe);
            }
        }

        [HttpPost("Midi")]
        public async Task<IActionResult> CreateSheetWithMidi([FromForm] SheetMidiCreateDto input)
        {
            ValidationResult valRe = new ValidationResult();
            try
            {
                await valRe.ValidateAsync(input, services);
                if (!valRe.IsValid)
                {
                    return BadRequest(valRe);
                }
                var created = await services.SheetService.CreateSheetAsync(input);
                return Ok(created);
            }
            catch (Exception ex)
            {
                valRe.AddError(ex.Message);
                return BadRequest(valRe);
            }
        }

        [HttpPost("Xml")]
        public async Task<IActionResult> CreateSheetWithXml([FromForm] SheetXmlCreateDto input)
        {
            ValidationResult valRe = new ValidationResult();
            try
            {
                await valRe.ValidateAsync(input, services);
                if (!valRe.IsValid)
                {
                    return BadRequest(valRe);
                }
                var created = await services.SheetService.CreateSheetAsync(input);
                return Ok(created);
            }
            catch (Exception ex)
            {
                valRe.AddError(ex.Message);
                return BadRequest(valRe);
            }
        }


        [HttpPost("MidiAndSymbol")]
        public async Task<IActionResult> CreateSheetWithMidiAndSymbol([FromForm] SheetMidiCreateDto input)
        {
            ValidationResult valRe = new ValidationResult();
            try
            {
                await valRe.ValidateAsync(input, services);
                if (!valRe.IsValid)
                {
                    return BadRequest(valRe);
                }
                var created = await services.SheetService.CreateSheetAsync(input);
                return Ok(created);
            }
            catch (Exception ex)
            {
                valRe.AddError(ex.Message);
                return BadRequest(valRe);
            }
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromForm] SheetUpdateDto input)
        {
            SheetGetDto dto = await services.SheetService.UpdateSheetAsync(input);
            return Ok(dto);
        }

        // DELETE api/<SheetsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteSheet(int id)
        {
            await services.SheetService.DeleteSheet(id);
            return Ok(new BaseResponse<string>("Delete sheet successfully", StatusCodes.Status200OK));
        }

        //Save Tracking point and data player
        [HttpPost("save-point")]
        public async Task<IActionResult> savePointPlayerBySong([FromForm] CreateTrackingDto trackingDto)
        {
            //get Username by principal Claims
            string username = User.GetUsername();
            User user = await services.UserService.GetUserByUserName(username);
            Sheet sheet = await services.SheetService.GetSheetByIdAsync<Sheet>(trackingDto.SheetId);
            await services.PlayTrackingService.createTrackingAsync(user, sheet, trackingDto.Point);
            return Created("", new BaseResponse<string>("Save point successfully!", 201));
        }

        [HttpGet("max-point/{id:int}")]
        public async Task<IActionResult> getMaxPointBySheetId([FromRoute] int id)
        {
            int maxPointId = await services.PlayTrackingService.getMaxPointBySheetId(id);
            return Ok(new BaseResponse<string>("Save point successfully!", 200, maxPointId+""));
        }
    }
}
