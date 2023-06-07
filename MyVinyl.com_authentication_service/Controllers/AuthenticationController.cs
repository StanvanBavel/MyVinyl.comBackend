using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyVinyl.com_authentication_service.Database.Datamodels.Dtos;
using MyVinyl.com_authentication_service.Helpers;
using MyVinyl.com_authentication_service.Services;

namespace MyVinyl.com_authentication_service.Controllers
{
        [ApiController]
        [Route("api/user")]
        public class AuthenticationController : Controller
        {
            private readonly IAuthService _service;


            public AuthenticationController(IAuthService service)
            {
                _service = service;


            }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> AddUser(UserRequest request)
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
            [Route("{id}")]
            public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
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
        }
    }