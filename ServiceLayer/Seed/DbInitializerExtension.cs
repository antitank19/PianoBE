using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLayer.Seed
{
    public static class DbInitializerExtension
    {
        public static IApplicationBuilder SeedInMemoryDb(this IApplicationBuilder app, bool isInMemory)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<PianoContext>();
                DbInitializer.Initialize(context, isInMemory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return app;
        }
        public class DbInitializer
        {
            internal static void Initialize(PianoContext context, bool isInMemory)
            {
                ArgumentNullException.ThrowIfNull(context, nameof(context));
                if (isInMemory)
                {
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
                    #region seed Artists
                    if (!context.Artists.Any())
                    {

                        context.Artists.AddRange(DbSeed.Artists);
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
                    if (!context.Artists.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Artists ON");
                            context.Artists.AddRange(DbSeed.Artists);
                            context.SaveChanges();
                            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Artists OFF");
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
