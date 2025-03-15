using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ModelViews;
using ServiceLayer.ModelViews.Genre;
using ServiceLayer.Services.Implementation;
using ServiceLayer.Services.Interface;

namespace API.Controllers;

[Route("api/genre")]
public class GenreController : ControllerBase
{
    private readonly IServiceWrapper _serviceWrapper;
    public GenreController(IServiceWrapper serviceWrapper)
    {
        _serviceWrapper = serviceWrapper;
    }
    
    /// <summary>
    /// Lấy thể loại nhạc dựa trên Id
    /// </summary>
    /// <param name="id">
    /// Id của thể loại nhạc (truyền vào param)
    /// </param>
    /// <returns></returns>
    [HttpGet]
    [Route("get-genre-by-id/{id}")]
    public async Task<ActionResult<BaseResponse<GetGenreResponse>>> GetById(int id)
    {
        var genre = await _serviceWrapper.GenreService.GetGenreById(id);
        return Ok(new BaseResponse<GetGenreResponse>("Get genre by Id successfully", StatusCodes.Status200OK, genre));
    }
    
    /// <summary>
    /// Lấy tất cả thể loại nhạc
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("get-all-genre")]
    public async Task<ActionResult<BaseResponse<IEnumerable<GetGenreResponse>>>> GetAllGenre()
    {
        var genres = await _serviceWrapper.GenreService.GetAllGenre();
        return Ok(new BaseResponse<IEnumerable<GetGenreResponse>>("Get all genre successfully", StatusCodes.Status200OK, genres));
    }
    
    /// <summary>
    /// Tạo mới thể loại nhạc, Chỉ có admin mới có quyền tạo mới
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("create-genre")]
    public async Task<ActionResult<BaseResponse<string>>> CreateNewGenre([FromBody] CreateGenreRequest request)
    {
        await _serviceWrapper.GenreService.CreateGenre(request);
        return Ok(new BaseResponse<string>("Create genre successfully", StatusCodes.Status201Created));
    }
    
    /// <summary>
    /// Cập nhật thể loại nhạc dựa trên Id, Chỉ có admin mới có quyền cập nhật
    /// </summary>
    /// <param name="id">
    /// Id của thể loại nhạc cần cập nhật
    /// </param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("update-genre/{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse<string>>> UpdateGenre(int id, [FromBody] UpdateGenreRequest request)
    {
        await _serviceWrapper.GenreService.UpdateGenre(id, request);
        return Ok(new BaseResponse<string>("Update genre successfully", StatusCodes.Status200OK));
    }

    [HttpDelete]
    [Route("delete-genre/{id}")]
    public async Task<ActionResult<BaseResponse<string>>> DeleteGenre(int id)
    {
        await _serviceWrapper.GenreService.DeleteGenre(id);
        return Ok(new BaseResponse<string>("Delete genre successfully", StatusCodes.Status200OK));
    }
}