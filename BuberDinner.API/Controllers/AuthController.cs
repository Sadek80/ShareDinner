using BuberDinner.Application.Services.Interfaces.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterUserRequest request)
        {
            var response = _authenticationService.Register(request);
            return Ok(response);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUserRequest request)
        {
            var response = _authenticationService.Login(request);
            return Ok(response);
        }
    }
}
