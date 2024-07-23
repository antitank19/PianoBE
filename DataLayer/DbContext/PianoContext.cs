using DataLayer.DbObject;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbContext
{
    public class PianoContext : IdentityDbContext<
        User, Role, int,
        UserClaim, UserRole, UserLogin,
        RoleClaim, UserToken>
    {
        public PianoContext(DbContextOptions<PianoContext> options) : base(options)
        { }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<Note> Notes { get; set; }                  
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Chord> Chords { get; set; }
        public DbSet<ChordNote> ChordNotes { get; set; }
        public DbSet<Instrument> Instruments { get; set; }

        //Identity
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region identity
            modelBuilder.Entity<User>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Role>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
            #endregion
            modelBuilder.Entity<Sheet>(e =>
            {
                e.HasMany(e => e.RightMeasures)
                    .WithOne(e => e.RightSheet)
                    .HasForeignKey(e => e.RightSheetId);
                    //.IsRequired();
                e.HasMany(e => e.LeftMeasures)
                    .WithOne(e => e.LeftSheet)
                    .HasForeignKey(e => e.LeftSheetId);
            });
        }
    }

}
