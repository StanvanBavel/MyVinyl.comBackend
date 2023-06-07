using Microsoft.EntityFrameworkCore;
using MyVinyl.com_authentication_service.com.Services;
using MyVinyl.com_authentication_service.Database.Contexts;
using MyVinyl.com_authentication_service.Database.Converters;
using MyVinyl.com_authentication_service.Database.Datamodels;
using MyVinyl.com_authentication_service.Database.Datamodels.Dtos;
using MyVinyl.com_authentication_service.Services;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetValue<string>("ConnectionString");
builder.Services.AddDbContextPool<UserContext>(
    options => options.UseSqlServer(connection));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


//Inject services.
builder.Services.AddTransient<IAuthService, AuthenticationService>();

//Inject converter.
builder.Services.AddScoped<IDtoConverter<User, UserRequest, UserResponse>, UserDtoConverter>();
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
