using Microsoft.AspNetCore.Identity;

namespace DataLayer.DbObject
{
    public class Role : IdentityRole<int>
    {
        public Role() : base()
        {

        }
        public Role(string roleName) : this()
        {
            Name = roleName;
        }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }

    }
}
