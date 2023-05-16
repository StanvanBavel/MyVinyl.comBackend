using EasyNetQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVinyl.com_rabbitmq.BackgroundServices;
using static MyVinyl.com_rabbitmq.BackgroundServices.UserRequest;

namespace MyVinyl.com_rabbitmq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IBus _bus;

        public ProducerController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<string> Post()
        {
            UserRequest request = new UserRequest(1);
            var response = await _bus.Rpc.RequestAsync<UserRequest, UserResponse>(request);
            return response.Name;
        }
    }
}
