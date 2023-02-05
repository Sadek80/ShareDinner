using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Repositories;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Localization;
using ErrorOr;
using Mediator;
using Microsoft.Extensions.Localization;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<UserResponse>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IStringLocalizer<ErrorLocalizer> _stringLocalizer;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository,
                                     IStringLocalizer<ErrorLocalizer> stringLocalizer)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async ValueTask<ErrorOr<UserResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
