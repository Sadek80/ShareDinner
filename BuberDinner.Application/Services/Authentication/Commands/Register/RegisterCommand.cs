using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Mediator;

namespace BuberDinner.Application.Services.Authentication.Commands.Register
{
    public record RegisterCommand : IRequest<ErrorOr<UserResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
