using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Mediator;

namespace BuberDinner.Application.Services.Authentication.Queries.Login
{
    public record LoginQuery : IRequest<ErrorOr<UserResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
