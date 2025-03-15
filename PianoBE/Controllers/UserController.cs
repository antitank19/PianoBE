using API.Extensions;
using AutoMapper;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.DTOs.Song;
using ServiceLayer.DTOs.User;
using ServiceLayer.ModelViews;
using ServiceLayer.ModelViews.Users;
using ServiceLayer.Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;
        public UserController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }
        [HttpGet("all")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUserByPage([Required] int pageNum, [Required] int pageSize, string? keyword)
        {
            var items = await _serviceWrapper.UserService.GetUserByPage(pageNum, pageSize, keyword);
            return Ok(items);
        }

        [HttpPost("add")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewUser([FromForm, Required] AddNewUserDto userDto)
        {
            User user = await _serviceWrapper.UserService.SaveUser(userDto);
            return Ok(new BaseResponse<User>("Save User with id " + user.Id + " successfully!", StatusCodes.Status200OK));
        }

        [HttpDelete("del/{id}")]
        public async Task<IActionResult> DeleteUserById([FromRoute] int id)
        {
            await _serviceWrapper.UserService.DeleteUserById(id);
            return Ok(new BaseResponse<User>("Delete User with id " + id + " successfully!", StatusCodes.Status200OK));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromForm, Required] UpdateUserDto userDto)
        {
            User user = await _serviceWrapper.UserService.UpdateUser(userDto);
            return Ok(new BaseResponse<User>("Update User with id " + user.Id + " successfully!", StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            UserDto user = await _serviceWrapper.UserService.GetUserById(id);
            return Ok(new BaseResponse<UserDto>("Get User with id " + user.Id + " successfully!", StatusCodes.Status200OK, user));
        }

        [HttpPost("add-favorite-songs/{songId}")]
        public async Task<IActionResult> AddFavoriteSong([FromRoute, Required] int songId)
        {
            string username = User.GetUsername();
            if (!string.IsNullOrEmpty(username))
            {
                var result = await _serviceWrapper.UserService.AddFavoriteSongAsync(username, songId);
                if (result)
                {
                    return Ok(new BaseResponse<string>("Added favorite song successfully!", StatusCodes.Status200OK));
                }
                return BadRequest(new BaseResponse<string>("Failed to add favorite song.", StatusCodes.Status400BadRequest));
            }
            return BadRequest(new BaseResponse<string>("Invalid user.", StatusCodes.Status400BadRequest));
        }

        [HttpGet("all-favorite-songs")]
        public async Task<IActionResult> GetAllFavoriteSongs(string? keyword)
        {
            string username = User.GetUsername();
            if (!string.IsNullOrEmpty(username))
            {
                var favoriteSongs = await _serviceWrapper.UserService.GetAllFavoriteSongsAsync(username, keyword);
                return Ok(new BaseResponse<List<FavoriteSongDto>>("Retrieved favorite songs successfully!", StatusCodes.Status200OK, favoriteSongs));
            }
            return BadRequest(new BaseResponse<string>("Invalid user.", StatusCodes.Status400BadRequest));
        }

        [HttpDelete("delete-favorite-songs/{songId}")]
        public async Task<IActionResult> RemoveFavoriteSong([FromRoute, Required] int songId)
        {
            string username = User.GetUsername();
            if (!string.IsNullOrEmpty(username))
            {
                var result = await _serviceWrapper.UserService.RemoveFavoriteSongAsync(username, songId);
                if (result)
                {
                    return Ok(new BaseResponse<string>("Deleted favorite song successfully!", StatusCodes.Status200OK));
                }
                return NotFound(new BaseResponse<string>("Song not found in favorites!", StatusCodes.Status404NotFound));
            }
            return BadRequest(new BaseResponse<string>("Invalid user.", StatusCodes.Status400BadRequest));
        }
    }
}
