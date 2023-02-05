using BuberDinner.Application.Services.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ApiController
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var registerCommand = new RegisterCommand()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
            };

            var response = await _sender.Send(registerCommand, cancellationToken);

            return response.Match(
                                    success => Ok(response.Value),
                                    errors => Problem(errors)
                                 );

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var loginQuery = new LoginQuery()
            {
                Email = request.Email,
                Password = request.Password
            };

            var response = await _sender.Send(loginQuery, cancellationToken);

            return response.Match(
                                    success => Ok(response.Value),
                                    errors => Problem(errors)
                                 );
        }
    }
}
