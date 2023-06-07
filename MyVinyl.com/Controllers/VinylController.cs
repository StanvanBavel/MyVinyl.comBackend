using Microsoft.AspNetCore.Mvc;
using MyVinyl.com.Database.Datamodels;
using MyVinyl.com.Database.Datamodels.Dtos;
using MyVinyl.com.Helpers;
using MyVinyl.com.Logging;
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
            
            logData data = new logData(request.Name, request.Description, request.Image);
            await logData.LogToMicroserviceAsync(data, "https://localhost:7218/Logging/vinyl");
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