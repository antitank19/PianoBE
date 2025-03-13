using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interface;
using ServiceLayer.ModelViews;
using ServiceLayer.ModelViews.Enums;
using ServiceLayer.ModelViews.Genre;
using ServiceLayer.ModelViews.Instruments;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;
        public InstrumentController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        /// <summary>
        /// Lấy nhạc cụ dựa trên Id
        /// </summary>
        /// <param name="id">
        /// Id của nhạc cụ (truyền vào param)
        /// </param>
        /// <returns></returns>
        [HttpGet("get-instrument-by-id/{id}")]
        public async Task<ActionResult<BaseResponse<ResponseInstrumentsModel>>> GetById(int id)
        {
            var instrument = await _serviceWrapper.InstrumentService.GetInstrumentById(id);
            return Ok(new BaseResponse<ResponseInstrumentsModel>("Get instrument by Id successfully", StatusCodes.Status200OK, instrument));
        }

        /// <summary>
        /// Lấy tất cả nhạc cụ
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-instrument")]
        public async Task<ActionResult<BaseResponse<IEnumerable<GetGenreResponse>>>> GetAllInstrument()
        {
            var instruments = await _serviceWrapper.InstrumentService.GetAllInstrumente();
            return Ok(new BaseResponse<IEnumerable<ResponseInstrumentsModel>>("Get all instrument successfully", StatusCodes.Status200OK, instruments));
        }

        /// <summary>
        /// Tạo mới nhạc cụ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create-instrument")]
        public async Task<ActionResult<BaseResponse<CreateInstrumentsModel>>> CreateNewInstrument([FromBody] CreateInstrumentsModel request)
        {
            var result = await _serviceWrapper.InstrumentService.CreateInstrument(request);
            return Ok(new BaseResponse<CreateInstrumentsModel>("Create instrument successfully", StatusCodes.Status201Created, result));
        }

        /// <summary>
        /// Cập nhật nhạc cụ dựa trên Id
        /// </summary>
        /// <param name="id">
        /// Id của nhạc cụ cần cập nhật
        /// </param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-instrument/{id}")]
        public async Task<ActionResult<BaseResponse<UpdateInstrumentsModel>>> UpdateInstrument(int id, [FromBody] UpdateInstrumentsModel request)
        {
            var instrument = await _serviceWrapper.InstrumentService.UpdateInstrument(id, request);
            return Ok(new BaseResponse<UpdateInstrumentsModel>("Update instrument successfully", StatusCodes.Status200OK, instrument));
        }


        /// <summary>
        /// Xóa nhạc cụ dựa trên Id
        /// </summary>
        /// <param name="id">
        /// Id của nhạc cụ cần xóa

        /// </param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-instrument/{id}")]
        public async Task<ActionResult> DeleteInstrument(int id)
        {
            await _serviceWrapper.InstrumentService.DeleteInstrument(id);
            return Ok(new BaseResponse<string>("Delete Instruments successfully", StatusCodes.Status200OK));
        }
    }
}
