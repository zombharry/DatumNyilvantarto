using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    
    public class SchoolDbContext :DbContext
    {
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<Student> Students { get; set; }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SchoolClass>().ToTable("SchoolClasses");

            modelBuilder.Entity<Student>()
                .ToTable("Students");

            modelBuilder.Entity<SchoolClass>()
            .HasMany(c => c.Students)
            .WithOne(s => s.SchoolClass)
            .HasForeignKey(s => s.ClassId);

        }
    }
}
