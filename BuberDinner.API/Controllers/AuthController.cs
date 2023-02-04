using BuberDinner.Application.Services.Interfaces.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var response = _authenticationService.Register(request);

            return response.Match(
                                    success => Ok(response.Value),
                                    errors => Problem(errors)
                                 );

        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUserRequest request)
        {
            var response = _authenticationService.Login(request);

            return response.Match(
                                    success => Ok(response.Value),
                                    errors => Problem(errors)
                                 );
        }
    }
}
