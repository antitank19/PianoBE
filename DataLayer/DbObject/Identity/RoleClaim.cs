using Microsoft.AspNetCore.Identity;

namespace DataLayer.DbObject
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public virtual Role Role { get; set; }
    }
}