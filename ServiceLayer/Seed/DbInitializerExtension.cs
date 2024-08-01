using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLayer.Seed
{
    public static class DbInitializerExtension
    {
        public static async Task<IApplicationBuilder> SeedInMemoryDb(this IApplicationBuilder app, bool isInMemory, bool seedOnStartup)
        {
            if (seedOnStartup)
            {
                ArgumentNullException.ThrowIfNull(app, nameof(app));

                using var scope = app.ApplicationServices.CreateScope();
                var services = scope.ServiceProvider;
                try
                {
                    PianoContext context = services.GetRequiredService<PianoContext>();
                    RoleManager<Role> roleManager = services.GetRequiredService<RoleManager<Role>>();
                    UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();
                    await DbInitializer.InitializeAsync(context, roleManager, userManager, isInMemory);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return app;
        }
        public class DbInitializer
        {
            internal static async Task InitializeAsync(PianoContext context, RoleManager<Role> roleManager, UserManager<User> userManager, bool isInMemory)
            {
                ArgumentNullException.ThrowIfNull(context, nameof(context));
                if (isInMemory)
                {
                    #region Genres
                    if (!context.Genres.Any())
                    {
                        context.Genres.AddRange(DbSeed.Genres);
                    }
                    #endregion
                    #region Roles
                    if (!context.Roles.Any())
                    {
                        foreach (var role in DbSeed.Roles)
                        {
                            await roleManager.CreateAsync(role);
                        }
                    }
                    #endregion
                    #region seed Instruments
                    if (!context.Instruments.Any())
                    {
                        context.Instruments.AddRange(DbSeed.Instruments);
                    }
                    #endregion
                    #region seed Notes
                    if (!context.Notes.Any())
                    {

                        context.Notes.AddRange(DbSeed.Notes);
                    }
                    #endregion
                    #region seed Users
                    if (!context.Users.Any())
                    {
                        foreach (var user in DbSeed.Admins)
                        {
                            var createdAdmin = await userManager.CreateAsync(user, "123456789");
                            if (createdAdmin.Succeeded)
                            {
                                await userManager.AddToRoleAsync(user, "Admin");
                            }
                        }
                        foreach (var user in DbSeed.Artists)
                        {
                            var createdArtists = await userManager.CreateAsync(user, "123456789");
                            if (createdArtists.Succeeded)
                            {
                                await userManager.AddToRoleAsync(user, "Artist");
                            }
                        }
                        foreach (var user in DbSeed.Players)
                        {
                            var createdPlayer = await userManager.CreateAsync(user, "123456789");
                            if (createdPlayer.Succeeded)
                            {
                                await userManager.AddToRoleAsync(user, "Player");
                            }
                        }
                    }
                    #endregion
                    #region seed Songs
                    if (!context.Songs.Any())
                    {

                        context.Songs.AddRange(DbSeed.Songs);
                    }
                    #endregion
                    #region seed Sheets
                    if (!context.Sheets.Any())
                    {

                        context.Sheets.AddRange(DbSeed.Sheets);
                    }
                    #endregion
                    #region seed Measures
                    if (!context.Measures.Any())
                    {

                        context.Measures.AddRange(DbSeed.Measures);
                    }
                    #endregion
                    #region seed Chords
                    if (!context.Chords.Any())
                    {

                        context.Chords.AddRange(DbSeed.Chords);
                    }
                    #endregion
                    #region seed ChordNote
                    if (!context.ChordNotes.Any())
                    {

                        context.ChordNotes.AddRange(DbSeed.ChordNotes);
                    }
                    #endregion
                    context.SaveChanges();
                }
                else
                {
                    #region seed Genres
                    if (!context.Genres.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Genres ON");
                            context.Genres.AddRange(DbSeed.Genres);
                            context.SaveChanges();
                            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Genres OFF");
                            transaction.Commit();
                        }
                    }
                    #endregion
                    #region seed Instruments
                    if (!context.Instruments.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Instruments ON");
                            context.Instruments.AddRange(DbSeed.Instruments);
                            context.SaveChanges();
                            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Instruments OFF");
                            transaction.Commit();
                        }
                    }
                    #endregion
                    #region Roles
                    if (!context.Roles.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            // Temporarily enable IDENTITY_INSERT for AspNetRoles
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT AspNetRoles ON");
                            foreach (var role in DbSeed.Roles)
                            {
                                var roleExist = await roleManager.RoleExistsAsync(role.Name);
                                if (!roleExist)
                                {
                                    var roleResult = await roleManager.CreateAsync(role);
                                    if (!roleResult.Succeeded)
                                    {
                                        throw new Exception($"Failed to create role {role.Name}");
                                    }
                                }
                            }

                            context.SaveChanges();

                            // Disable IDENTITY_INSERT for AspNetRoles
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT AspNetRoles OFF");
                            transaction.Commit();
                        }
                    }
                    #endregion
                    #region seed Notes
                    if (!context.Notes.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Notes ON");
                            context.Notes.AddRange(DbSeed.Notes);
                            context.SaveChanges();
                            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Notes OFF");
                            transaction.Commit();
                        }
                    }
                    #endregion
                    #region seed Artists
                    if (!context.Users.Any())
                    {
                        //using (var transaction = context.Database.BeginTransaction())
                        //{
                        //    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Artists ON");
                        //    context.Users.AddRange(DbSeed.Artists);
                        //    context.SaveChanges();
                        //    //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Artists OFF");
                        //    transaction.Commit();
                        //}
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT AspNetUsers ON");

                            foreach (var user in DbSeed.Admins)
                            {
                                var createdAdmin = await userManager.CreateAsync(user, "123456789");
                                if (createdAdmin.Succeeded)
                                {
                                    await userManager.AddToRoleAsync(user, "Admin");
                                }
                            }
                            foreach (var user in DbSeed.Artists)
                            {
                                var createdArtists = await userManager.CreateAsync(user, "123456789");
                                if (createdArtists.Succeeded)
                                {
                                    await userManager.AddToRoleAsync(user, "Artist");
                                }
                            }
                            foreach (var user in DbSeed.Players)
                            {
                                var createdPlayer = await userManager.CreateAsync(user, "123456789");
                                if (createdPlayer.Succeeded)
                                {
                                    await userManager.AddToRoleAsync(user, "Player");
                                }
                            }
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT AspNetUsers OFF");
                            transaction.Commit();
                        }
                    }
                    #endregion
                    #region seed Songs
                    if (!context.Songs.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Songs ON");
                            context.Songs.AddRange(DbSeed.Songs);
                            context.SaveChanges();
                            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Songs OFF");
                            transaction.Commit();
                        }
                    }
                    #endregion
                    #region seed Sheets
                    if (!context.Sheets.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Sheets ON");
                            context.Sheets.AddRange(DbSeed.Sheets);
                            context.SaveChanges();
                            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Sheets OFF");
                            transaction.Commit();
                        }
                    }
                    #endregion
                    #region seed Mesuare
                    if (!context.Measures.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Measures ON");
                            context.Measures.AddRange(DbSeed.Measures);
                            context.SaveChanges();
                            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Sheets OFF");
                            transaction.Commit();
                        }
                    }
                    #endregion
                    #region seed Chords
                    if (!context.Chords.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Chords ON");
                            context.Chords.AddRange(DbSeed.Chords);
                            context.SaveChanges();
                            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Chords OFF");
                            transaction.Commit();
                        }
                    }
                    #endregion
                    #region seed ChordNotes
                    if (!context.ChordNotes.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ChordNotes ON");
                            context.ChordNotes.AddRange(DbSeed.ChordNotes);
                            context.SaveChanges();
                            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT SongNotes OFF");
                            transaction.Commit();
                        }
                    }
                    #endregion
                }
            }
        }
    }
}
