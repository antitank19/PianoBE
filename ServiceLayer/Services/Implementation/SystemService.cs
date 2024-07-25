using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services.Interface;
using ServiceLayer.Utils.EntityFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation
{
    public class SystemService : ISystemService
    {
        private readonly PianoContext context;

        public SystemService(PianoContext context)
        {
            this.context = context;
            //this.userManager = userManager;
        }

        public async Task NukeOld()
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("Start Nuke", start);

            // Create new stopwatch
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            // Begin timing
            stopwatch.Start();

            //bool tryAgain = true;
            //while (tryAgain)
            //{
                //try
                {
                    //dbContext.Artists.Clear();

                    //way faster
                    #region identity
                    context.RoleClaims.Delete();
                    context.UserRoles.Delete();
                    context.UserLogins.Delete();
                    context.UserClaims.Delete();
                    context.UserTokens.Delete();
                    context.Roles.Delete();
                    context.Users.Delete();
                    #endregion
                    context.Notes.Delete();
                    context.Instruments.Delete();
                    context.Songs.Delete();
                    context.Sheets.Delete();
                    context.Measures.Delete();
                    context.ChordNotes.Delete();
                    context.Chords.Delete();

                    //tryAgain = false;
                }
                //catch { }

                // Stop timing
                stopwatch.Stop();

                Console.WriteLine("Time taken : {0}", stopwatch.Elapsed);

                DateTime end = DateTime.Now;
                Console.WriteLine("End Nuke", end);
                Console.WriteLine("Time Nuke", (start - end).TotalMilliseconds);

            //}
        }
        public async Task Nuke()
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("Start Nuke", start);

            // Create new stopwatch
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            // Begin timing
            stopwatch.Start();

            #region MySql
            //// Disable foreign key checks
            //await context.Database.ExecuteSqlRawAsync("SET FOREIGN_KEY_CHECKS = 0");

            //// Delete all data
            //foreach (var entityType in context.Model.GetEntityTypes())
            //{
            //    var tableName = entityType.GetTableName();
            //    await context.Database.ExecuteSqlRawAsync($"DELETE FROM {tableName}");
            //}

            //// Enable foreign key checks
            //await context.Database.ExecuteSqlRawAsync("SET FOREIGN_KEY_CHECKS = 1");

            //await context.SaveChangesAsync();
            #endregion

            #region SQLServer
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Disable all foreign key constraints
                    var tables = context.Model.GetEntityTypes()
                        .Select(t => t.GetTableName())
                        .Distinct()
                        .ToList();

                    foreach (var table in tables)
                    {
                        context.Database.ExecuteSqlRaw($"ALTER TABLE [{table}] NOCHECK CONSTRAINT ALL");
                    }

                    // Delete all data
                    foreach (var table in tables)
                    {
                        context.Database.ExecuteSqlRaw($"DELETE FROM [{table}]");
                    }

                    // Re-enable all foreign key constraints
                    foreach (var table in tables)
                    {
                        context.Database.ExecuteSqlRaw($"ALTER TABLE [{table}] WITH CHECK CHECK CONSTRAINT ALL");
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            #endregion

            // Stop timing
            stopwatch.Stop();

            Console.WriteLine("Time taken : {0}", stopwatch.Elapsed);

            DateTime end = DateTime.Now;
            Console.WriteLine("End Nuke", end);
            Console.WriteLine("Time Nuke", (start - end).TotalMilliseconds);

        }
    }
}
