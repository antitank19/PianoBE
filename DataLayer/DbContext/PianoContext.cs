using DataLayer.DbObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbContext
{
    public class PianoContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PianoContext(DbContextOptions<PianoContext> options) : base(options)
        { }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<SongNote> SongNotes { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
