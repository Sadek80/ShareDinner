using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Repositories;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Localization;
using ErrorOr;
using Mediator;
using Microsoft.Extensions.Localization;
using BuberDinner.Domain.Common.Errors;

namespace BuberDinner.Application.Services.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<UserResponse>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IStringLocalizer<ErrorLocalizer> _stringLocalizer;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository,
                                     IStringLocalizer<ErrorLocalizer> stringLocalizer)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async ValueTask<ErrorOr<UserResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
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
    }
}
