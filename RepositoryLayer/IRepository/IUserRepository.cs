using DataLayer.DbObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> addNewUser(User user, string role);
        Task<IQueryable<User>> getAllUserRolesAsync(string keyword);
        Task<bool> isUserNameExist(string name);
        Task UpdateUserAsync(User user, bool isNewPassword);
    }
}
