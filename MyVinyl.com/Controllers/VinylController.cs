using Microsoft.AspNetCore.Mvc;
using MyVinyl.com.Database.Datamodels;
using MyVinyl.com.Database.Datamodels.Dtos;
using MyVinyl.com.Helpers;
using MyVinyl.com.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MyVinyl.com.Controllers
{
      
[ApiController]
[Route("api/vinyls")]
public class VinylController : Controller
{
    private readonly IVinylService _service;

    public VinylController(IVinylService service)
    {
        _service = service;
            
    }

    [HttpPost]
    public async Task<ActionResult<VinylResponse>> AddVinyl(VinylRequest request)
    {
        try
        {
            return Ok(await _service.AddAsync(request));
        }
        catch (DuplicateException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<VinylResponse>>> GetAllVinyls()
    {
        return Ok(await _service.GetAllAsync());

            //Vinyl vinyl = new Vinyl();
            //vinyl id = Guid.NewGuid().ToString();
            //vinyl.name = "test";

            //ConnectionFactory factory = new();
            //factory.Uri = new Uri("amqps://rooqhdeb:wqZzav90erRV5x_N64xzfAnyKrviqnMR@rat.rmq2.cloudamqp.com/rooqhdeb");
            //factory.ClientProvidedName = "VinylService";

            //IConnection cnn = factory.CreateConnection();
            //IModel channel = cnn.CreateModel();
            //string exchangeName = "MyVinyl.com";
            //string routingKey = "vinyl-routing-key";
            //string queueName = "VinylQueue";



            //channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            //channel.QueueDeclare(queueName, true, false, false, null);
            //channel.QueueBind(queueName, exchangeName, routingKey, null);



            //var json = JsonConvert.SerializeObject(Vinyl);
            //var body = Encoding.UTF8.GetBytes(json);
            //channel.BasicPublish(exchangeName, routingKey, null, body);
            //_logger.LogInformation($"Message published to {queueName}");



            //channel.Close();
            //cnn.Close();



            //return Ok(vinyl);
        }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<VinylResponse>> GetVinylById(Guid id)
    {
        try
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<VinylResponse>> GetVinylByName(string name)
    {
        try
        {
            return Ok(await _service.GetByNameAsync(name));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<VinylResponse>> UpdateVinyl(Guid id, VinylRequest request)
    {
        try
        {
            return Ok(await _service.UpdateAsync(id, request));
        }
        catch (DuplicateException e)
        {
            return Conflict(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<VinylResponse>> DeleteVinylById(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
}