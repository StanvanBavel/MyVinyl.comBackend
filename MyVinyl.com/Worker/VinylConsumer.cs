using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Channels;
using System.Text;
using Newtonsoft.Json;
using MyVinyl.com.Database.Datamodels;

namespace MyVinyl.com.Worker
{
    public class VinylConsumer : BackgroundService
    {
    
    private readonly ILogger<VinylConsumer> _logger;
    //private readonly IOrderService _service;

    public readonly IServiceScopeFactory _serviceScopeFactory;

    private string exchangeName;
    private string routingKey;
    private string queueName;
    private IModel channel;

    public VinylConsumer(ILogger<VinylConsumer> logger, IServiceScopeFactory serviceScopeFactory)
    {
        ConnectionFactory factory = new();
        factory.Uri = new Uri("amqps://rooqhdeb:wqZzav90erRV5x_N64xzfAnyKrviqnMR@rat.rmq2.cloudamqp.com/rooqhdeb");
        factory.ClientProvidedName = "VinylService";

        IConnection cnn = factory.CreateConnection();
        channel = cnn.CreateModel();
        exchangeName = "MyVinyl.com";
        routingKey = "vinyl-routing-key";
        queueName = "VinylQueue";

        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        // When the timer should have no due-time, then do the work once now.

        DoWork();

        using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                DoWork();
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
        }
    }

    private void DoWork()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) =>
            {
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                var body = args.Body.ToArray();

                string message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Message Received: {message}");
                Vinyl order = JsonConvert.DeserializeObject<Vinyl>(message);

                //orderService.CreateOrder(order);
                //Sturen naar DB
                channel.BasicAck(args.DeliveryTag, false);
            };

            string consumerTag = channel.BasicConsume(queueName, false, consumer);
        }
    }

}
}
