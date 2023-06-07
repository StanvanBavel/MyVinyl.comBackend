using Microsoft.AspNetCore.Mvc;
using MyVinyl.com_logging.Database.Data;
using MyVinyl.com_logging.Services;

namespace MyVinyl.com_logging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly LoggingService _loggingService;

        public LoggingController(IConfiguration configuration)
        {
            _loggingService = new LoggingService(configuration);
        }

        [HttpPost("error")]
        public IActionResult VinylServiceDataError([FromBody] VinylServiceData logData)
        {
            // Log the error
            _loggingService.VinylServiceDataToDatabase(logData);

            // Return a success response
            return Ok();
        }

        [HttpPost("warning")]
        public IActionResult VinylServiceDataWarning([FromBody] VinylServiceData logData)
        {
            // Log the warning
            _loggingService.VinylServiceDataToDatabase(logData);

            // Return a success response
            return Ok();
        }

        [HttpPost("info")]
        public IActionResult VinylServiceDataInfo([FromBody] VinylServiceData logData)
        {
            // Log the informational message
            _loggingService.VinylServiceDataToDatabase(logData);

            // Return a success response
            return Ok();
        }

        [HttpPost("vinyl")]
        public IActionResult AddVinyl([FromBody] VinylServiceData logData)
        {
            _loggingService.VinylServiceDataToDatabase(logData);

            // Return a success response
            return Ok();
        }
    }
}
