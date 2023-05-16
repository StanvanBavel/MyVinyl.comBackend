

using EasyNetQ;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        string rabbitmqConnectionString = "host=host.docker.internal;username=guest;password=guest;timeout=60";

        var bus = RabbitHutch.CreateBus(rabbitmqConnectionString);
        builder.Services.AddSingleton(bus);

        builder.Services.AddHostedService<MyVinyl.com_rabbitmq.BackgroundServices.UserEventHandler>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}