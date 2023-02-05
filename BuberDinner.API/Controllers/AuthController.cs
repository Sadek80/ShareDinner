using BuberDinner.Application.Services.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ApiController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public AuthController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var registerCommand = _mapper.Map<RegisterCommand>(request);

            var response = await _sender.Send(registerCommand, cancellationToken);

            return response.Match(success => Ok(response.Value),
                                  errors => Problem(errors));

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var loginQuery = _mapper.Map<LoginQuery>(request);
                
            var response = await _sender.Send(loginQuery, cancellationToken);

            return response.Match(success => Ok(response.Value),
                                  errors => Problem(errors));
        }
    }
}
