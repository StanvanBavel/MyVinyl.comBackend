using Microsoft.EntityFrameworkCore;
using MyVinyl.com_logging.Database.Data;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MyVinyl.com_logging.Database.Contexts
{
        public class LoggingDbContext : DbContext
        {
            public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
            {
            }

            public DbSet<VinylServiceData> VinylServiceEntries { get; set; }

        }
}
