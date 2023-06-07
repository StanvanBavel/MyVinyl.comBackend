using Microsoft.EntityFrameworkCore;
using MyVinyl.com_logging.Database.Contexts;
using MyVinyl.com_logging.Database.Data;

namespace MyVinyl.com_logging.Services
{
    public class LoggingService
    {
        private readonly LoggingDbContext _dbContext;

        public LoggingService(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LoggingDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConnectionString"));

            _dbContext = new LoggingDbContext(optionsBuilder.Options);
        }

        public void VinylServiceDataToDatabase(VinylServiceData vinylServiceData)
        {
            // Create a new LogEntry object to store in the database
            var logEntry = new VinylServiceData
            {
                Id = vinylServiceData.Id,
                Name = vinylServiceData.Name,
                Description = vinylServiceData.Description,
                Image = vinylServiceData.Image,
                IsActive = vinylServiceData.IsActive
            };

            // Add the LogEntry to the database and save changes
            _dbContext.VinylServiceEntries.Add(logEntry);
            _dbContext.SaveChanges();
        }
    }
}