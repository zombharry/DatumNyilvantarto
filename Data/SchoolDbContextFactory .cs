using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class SchoolDbContextFactory : IDesignTimeDbContextFactory<SchoolDbContext>
    {
        public SchoolDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolDbContext>();

            var exePath = AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = Path.Combine(exePath, "schoolDb.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new SchoolDbContext(optionsBuilder.Options);
        }
    }
}
