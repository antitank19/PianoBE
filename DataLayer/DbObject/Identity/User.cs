using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.DbObject
{
    public class User : IdentityUser<int>
    {
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        //public virtual ICollection<UserClaim> UserClaims { get; set; }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        //public string Username { get; set; }
        //public string Password { get; set; }
        //public string Email { get; set; }


        public ICollection<Song> Songs { get; set; }
    }
}
