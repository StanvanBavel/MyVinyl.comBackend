using Microsoft.EntityFrameworkCore;
using MyVinyl.com.Database.Configurations;
using MyVinyl.com.Database.Datamodels;

namespace MyVinyl.com.Database.Contexts
{
    public class VinylContext : DbContext
    {
        public VinylContext(DbContextOptions<VinylContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VinylConfiguration());
 
        }

        public DbSet<Vinyl> Vinyls { get; set; }
    }
}
