using Microsoft.EntityFrameworkCore;
using MyVinyl.com_authentication_service.Database.Configurations;
using MyVinyl.com_authentication_service.Database.Datamodels;

namespace MyVinyl.com_authentication_service.Database.Contexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
 
        }

        public DbSet<User> Users { get; set; }
    }
}
