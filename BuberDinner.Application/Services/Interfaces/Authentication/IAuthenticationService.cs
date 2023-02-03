using BuberDinner.Contracts.Authentication;

namespace BuberDinner.Application.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        UserResponse Register(RegisterUserRequest request);
        UserResponse Login(LoginUserRequest request);
    }
}
