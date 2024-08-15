using DataLayer.EnumsAndConsts;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.DbObject
{
    public class User : IdentityUser<int>
    {
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public string? EmailCode { get; set; }
        public DateTime? CodeGeneratedTime { get; set; }
        public string? Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Image { get; set; }
        public LoginTypeEnum LoginTypeEnum { get; set; }
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
