using DataLayer.DbObject;
using ServiceLayer.DTOs;
using ServiceLayer.DTOs.Song;
using ServiceLayer.DTOs.User;
using ServiceLayer.ModelViews.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface.Db
{
    public interface IUserService
    {
        Task<int> CountArtists();
        Task<int> CountUsers();
        Task DeleteUserById(int id);
        Task<User?> GetUserByEmail(string email);
        Task<UserDto> GetUserById(int id);
        Task<UserPageDto> GetUserByPage(int pageNum, int pageSize, string keyword);
        Task<User> GetUserByUserName(string username);
        Task<User> SaveUser(AddNewUserDto userDto);
        Task<User> UpdateUser(UpdateUserDto userDto);

        /*Task AddFavoriteSongAsync(string username, int songId);
        Task<List<FavoriteSongDto>> GetAllFavoriteSongsAsync(string userName);
        Task DeleteFavoriteSongAsync(string userName, int songId);*/

        Task<bool> AddFavoriteSongAsync(string username, int songId);
        Task<bool> RemoveFavoriteSongAsync(string username, int songId);
        Task<List<FavoriteSongDto>> GetAllFavoriteSongsAsync(string username, string? keyword);
    }
}
