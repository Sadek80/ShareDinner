using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Repositories;
using BuberDinner.Application.Services.Interfaces.Authentication;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public UserResponse Login(LoginUserRequest request)
        {
            var user = _userRepository.GetUserByEmail(request.Email);

            if (user is null || request.Password != user.Password)
                throw new Exception("Invalid Email or Password");

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

        public UserResponse Register(RegisterUserRequest request)
        {
            if (_userRepository.GetUserByEmail(request.Email) is not null)
                throw new Exception("User is already exist");

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
