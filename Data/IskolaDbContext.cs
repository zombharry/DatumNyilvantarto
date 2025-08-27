using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    
    public class IskolaDbContext :DbContext
    {
        public DbSet<Osztaly> Osztalyok { get; set; }
        public DbSet<Tanulo> Tanulok { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var logFile = "ef-log.txt";
                var writer = new StreamWriter(logFile, append: true) { AutoFlush = true };
                optionsBuilder.UseSqlite("Data Source=iskolaDb.db")
                    .EnableSensitiveDataLogging()
                    .LogTo(writer.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Osztaly>().ToTable("Osztalyok");

            modelBuilder.Entity<Tanulo>()
                .ToTable("Tanulok");

            modelBuilder.Entity<Osztaly>()
            .HasMany(c => c.Tanulok)
            .WithOne(s => s.Osztaly)
            .HasForeignKey(s => s.OsztalyId);

        }
    }
}
