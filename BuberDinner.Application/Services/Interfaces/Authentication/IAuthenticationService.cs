using BuberDinner.Contracts.Authentication;
using ErrorOr;

namespace BuberDinner.Application.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        ErrorOr<UserResponse> Register(RegisterUserRequest request);
        ErrorOr<UserResponse> Login(LoginUserRequest request);
    }
}
