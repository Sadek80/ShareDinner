using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Repositories;
using BuberDinner.Application.Services.Interfaces.Authentication;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using Microsoft.Extensions.Localization;
using BuberDinner.Domain.Common.Localization;

namespace BuberDinner.Application.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IStringLocalizer<ErrorLocalizer> _stringLocalizer;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository,
                                     IStringLocalizer<ErrorLocalizer> stringLocalizer)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _stringLocalizer = stringLocalizer;
        }

        public ErrorOr<UserResponse> Login(LoginUserRequest request)
        {
            var user = _userRepository.GetUserByEmail(request.Email);

            if (user is null || request.Password != user.Password)
            {
                return new[] 
                {
                    Error.Validation(_stringLocalizer[Errors.Authentication.InvalidCredentialsCode],
                                     _stringLocalizer[Errors.Authentication.InvalidCredentialsDescription]),

                    Error.Validation(_stringLocalizer[Errors.Authentication.UnAuthorizedCode],
                                     _stringLocalizer[Errors.Authentication.UnAuthorizedDescription])
                };
            }


            var userResponse = new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = _jwtTokenGenerator.GenerateToken(user)
            };

            return userResponse;
        }

        public ErrorOr<UserResponse> Register(RegisterUserRequest request)
        {
            if (_userRepository.GetUserByEmail(request.Email) is not null)
                return Error.Conflict(_stringLocalizer[Errors.User.DuplicateEmailCode], 
                                      _stringLocalizer[Errors.User.DuplicateEmailDescription]);

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
            };

            _userRepository.AddUser(user);

            var userResponse = new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = _jwtTokenGenerator.GenerateToken(user)
            };

            return userResponse;
        }
    }
}
