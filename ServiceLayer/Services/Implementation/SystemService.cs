using DataLayer.DbContext;
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
        private readonly PianoContext dbContext;

        public SystemService(PianoContext context)
        {
            this.dbContext = context;
        }

        public async Task Nuke()
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("Start Nuke", start);

            // Create new stopwatch
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            // Begin timing
            stopwatch.Start();

            bool tryAgain = true;
            while (tryAgain)
            {
                try
                {
                    //dbContext.Artists.Clear();

                    //way faster
                    dbContext.Artists.Delete();
                    dbContext.Notes.Delete();
                    dbContext.Instruments.Delete();
                    dbContext.Songs.Delete();
                    dbContext.Sheets.Delete();
                    dbContext.Measures.Delete();
                    dbContext.SongNotes.Delete();

                    tryAgain = false;
                }
                catch { }

                // Stop timing
                stopwatch.Stop();

                Console.WriteLine("Time taken : {0}", stopwatch.Elapsed);

                DateTime end = DateTime.Now;
                Console.WriteLine("End Nuke", end);
                Console.WriteLine("Time Nuke", (start - end).TotalMilliseconds);

            }
        }
    }
}
