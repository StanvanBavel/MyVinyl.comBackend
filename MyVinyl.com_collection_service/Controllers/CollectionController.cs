using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using MyVinyl.com_collection_service.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MyVinyl.com_collection_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public CollectionController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Vinyl vinyl = new Vinyl();
            vinyl.Id = Guid.NewGuid().ToString();
            vinyl.Name = "test";

            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqps://rooqhdeb:wqZzav90erRV5x_N64xzfAnyKrviqnMR@rat.rmq2.cloudamqp.com/rooqhdeb");
            factory.ClientProvidedName = "CollectionService";

            IConnection cnn = factory.CreateConnection();
            IModel channel = cnn.CreateModel();
            string exchangeName = "MyVinyl.com";
            string routingKey = "vinyl-routing-key";
            string queueName = "VinylQueue";



            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);



            var json = JsonConvert.SerializeObject(vinyl);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchangeName, routingKey, null, body);
            _logger.LogInformation($"Message published to {queueName}");


            channel.Close();
            cnn.Close();

            return Ok(vinyl);
        }
    }
}
