using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {   
        private readonly UserManager<User> _userManager;
        public UserRepository(PianoContext dbContext, UserManager<User> userManager) : base(dbContext)
        {
            _userManager = userManager;
        }

        public async Task<User> addNewUser(User user, string role)
        {
            var savedUser = await _userManager.CreateAsync(user, user.PasswordHash);
            await _userManager.AddToRoleAsync(user, role);
            if(savedUser.Succeeded) 
            {
                return user;
            }
            return null;
        }

        public async Task<IQueryable<User>> getAllUserRolesAsync(string keyword)
        {
            if(string.IsNullOrWhiteSpace(keyword))
            {
                return base._dbSet.AsQueryable()
                    .Include(user => user.UserRoles).ThenInclude(role => role.Role);
            }
            return base._dbSet.AsQueryable()
                .Where(user => user.Name.Contains(keyword))
                .Include(user => user.UserRoles).ThenInclude(role => role.Role);
        }

        public async Task<bool> isUserNameExist(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return user!=null;
        }

        public async Task UpdateUserAsync(User user, bool isNewPassword)
        {
            if (string.IsNullOrEmpty(user.SecurityStamp))
            {
                user.SecurityStamp = Guid.NewGuid().ToString(); // Example: Generate a new security stamp
            }
            if(isNewPassword)
            {
                var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
                user.PasswordHash = newPasswordHash;
            }
            var user1 = await _userManager.UpdateAsync(user);
        }
    }
}
