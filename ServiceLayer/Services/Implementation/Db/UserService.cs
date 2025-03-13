using AutoMapper;
using DataLayer.DbObject;
using DataLayer.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.IRepository;
using RepositoryLayer.Repository;
using ServiceLayer.CustomException;
using ServiceLayer.DTOs;
using ServiceLayer.DTOs.Song;
using ServiceLayer.DTOs.User;
using ServiceLayer.ModelViews.Users;
using ServiceLayer.PaggingItems;
using ServiceLayer.Services.Interface.Db;
using ServiceLayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation.Db
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration config;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, RoleManager<Role> roleManager, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleManager = roleManager;
            this.config = config;
        }

        public async Task<int> CountArtists()
        {
            int number = await _unitOfWork.GetRepository<User>().Entities
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .CountAsync(u => u.UserRoles.Any(ur => ur.Role.Name == "Artist"));
            return number;
        }

        public async Task<int> CountUsers()
        {
            int number = await _unitOfWork.UserRepository.CountAsync();
            return number;
        }

        public async Task DeleteUserById(int id)
        {
            await _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            User? user = await _unitOfWork.GetRepository<User>().Entities.SingleOrDefaultAsync(user => user.Email == email);
            return user;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            IQueryable<User> usersQuery = await _unitOfWork.UserRepository.getAllUserRolesAsync(null);
            var user = await usersQuery.SingleOrDefaultAsync(user => user.Id == id);
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth.ToString(),
                Email = user.Email,
                Roles = user.UserRoles.Select(role => role.Role.Name).ToList(),
                Image = user.Image,
            };
            return userDto;
        }

        public async Task<UserPageDto> GetUserByPage(int pageNum, int pageSize, string keyword)
        {
            IQueryable<User> usersQuery = null;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                usersQuery = _unitOfWork.GetRepository<User>().Entities.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role);
            }
            else
            {
                usersQuery = _unitOfWork.GetRepository<User>().Entities.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.UserName.Contains(keyword));
            }
            usersQuery = usersQuery.OrderBy(u => u.Id);
            PaginatedList<User> paginatedList = await _unitOfWork.UserRepository.GetPagging(usersQuery, pageNum, pageSize);

            var userDtos = paginatedList.Items.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth.ToString(),
                Email = user.Email,
                Image = user.Image,
                Roles = user.UserRoles.Select(role => role.Role.Name).ToList()
            }).ToList();

            UserPageDto userPageDto = new UserPageDto
            {
                TotalPage = paginatedList.TotalPages,
                PageNum = paginatedList.PageNumber,
                Users = userDtos
            };

            return userPageDto;
        }

        public async Task<User> GetUserByUserName(string username)
        {
            IQueryable<User> usersQuery = await _unitOfWork.GetRepository<User>().GetAllQueryableAsync();
            var user = await usersQuery.SingleOrDefaultAsync(user => user.UserName == username);
            if(user == null)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "username"));
            }
            return user;
        }

        public async Task<User> SaveUser(AddNewUserDto userDto)
        {
            User savedUser = null;
            User user = _mapper.Map<User>(userDto);
            //check role exist
            var roleExist = await _roleManager.RoleExistsAsync(userDto.Role);
            if (!roleExist)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "Role"));
            }
            //get User in Db to compare userName
            User? userInDb = await _unitOfWork.GetRepository<User>().Entities.FirstOrDefaultAsync(user => user.UserName == userDto.UserName);
            if (userInDb != null)
            {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.DUPLICATE, ErrorMessages.DUPLICATE.Replace("0", "Username"));
            }
            if (userDto.Image != null)
            {
                string imgUrl = await FirebaseStorageUtil.UploadFileAsync(userDto.Image, "Image/User", config["Firebase:StorageBucket"]);
                user.Image = imgUrl;
            }
            await _unitOfWork.UserRepository.addNewUser(user, userDto.Role);
            savedUser = user;
            await _unitOfWork.SaveAsync();
            return savedUser;
        }

        public async Task<User> UpdateUser(UpdateUserDto userDto)
        {
            User savedUser = null;
            User user = _mapper.Map<User>(userDto);
            //get User in Db to compare user
            User userInDb = await _unitOfWork.UserRepository.GetByIdAsync(userDto.Id);
            if (userInDb == null)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "User"));
            }
            //User update username must not exist in db
            if (!user.UserName.Equals(userInDb.UserName))
            {
                bool userExist = await _unitOfWork.UserRepository.isUserNameExist(user.UserName);
                if (userExist)
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.DUPLICATE, ErrorMessages.DUPLICATE.Replace("0", "Username"));
                }
            }
            string oldPassword = userInDb.PasswordHash;
            string newPassword = userDto.PasswordHash;
            //check is newPassword if user input have password, old if user leave it blank
            bool isNewPassword = true;
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                userDto.PasswordHash = oldPassword;
                isNewPassword = false;
            }
            _mapper.Map(userDto, userInDb);

            if (userDto.Image != null)
            {
                string imgUrl = await FirebaseStorageUtil.UploadFileAsync(userDto.Image, "Image/User", config["Firebase:StorageBucket"]);
                userInDb.Image = imgUrl;
            }

            await _unitOfWork.UserRepository.UpdateUserAsync(userInDb, isNewPassword);
            savedUser = user;
            await _unitOfWork.SaveAsync();
            return savedUser;
        }

        public async Task<bool> AddFavoriteSongAsync(string username, int songId)
        {
            var user = await GetUserByUserName(username);
            if (user == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "User"));
            }

            var song = await _unitOfWork.GetRepository<Song>().GetByIdAsync(songId);
            if (song == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "Song"));
            }

            if (user.FavoriteSong.Any(s => s.Id == songId))
            {
                return false; // Song already a favorite
            }

            user.FavoriteSong.Add(song);

            var userRepository = _unitOfWork.GetRepository<User>();
            userRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> RemoveFavoriteSongAsync(string username, int songId)
        {
            var user = await GetFavoriteByUser(username);

            if (user == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "User"));
            }

            var song = user.FavoriteSong.SingleOrDefault(s => s.Id == songId);
            if (song == null)
            {
                return false; // Song not found in favorites
            }

            user.FavoriteSong.Remove(song);
            _unitOfWork.GetRepository<User>().Update(user);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<List<FavoriteSongDto>> GetAllFavoriteSongsAsync(string username, string? keyword)
        {
            User user = null;
            if (keyword == null)
            {
                user = await GetFavoriteByUser(username);
            }
            else
            {
                user = await GetFavoriteByUser(username, keyword);
            }

            if (user == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "User"));
            }

            var favoriteSongs = user.FavoriteSong.Select(song => new FavoriteSongDto
            {
                Id = song.Id,
                Title = song.Title,
                Composer = song.Composer,
                ArtistName = _unitOfWork.GetRepository<Song>().Entities
                    .Include(s => s.Artist)
                    .Where(s => s.Id == song.Id)
                    .Select(s => s.Artist.Name).FirstOrDefault(),
                Genres = song.Genres.Select(g => g.Name.ToString()).ToList(),
                Image = song.Image
            }).ToList();

            return favoriteSongs;
        }

        public async Task<User> GetFavoriteByUser(string username)
        {
            var usersQuery = _unitOfWork.GetRepository<User>().Entities
                                .Include(u => u.FavoriteSong)
                                    .ThenInclude(u => u.Genres)
                                .Where(user => user.UserName == username);

            var user = await usersQuery.SingleOrDefaultAsync();

            if (user == null)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "username"));
            }

            return user;
        }

        public async Task<User> GetFavoriteByUser(string username, string keyword)
        {
            var usersQuery = _unitOfWork.GetRepository<User>().Entities
                                .Include(u => u.FavoriteSong)
                                    .ThenInclude(u => u.Genres)
                                .Where(user => user.UserName == username && user.Songs.Any(song => song.Title.Contains(keyword)));

            var user = await usersQuery.SingleOrDefaultAsync();

            if (user == null)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.NOT_FOUND, ErrorMessages.NOT_FOUND.Replace("0", "username"));
            }

            return user;
        }
    }
}
