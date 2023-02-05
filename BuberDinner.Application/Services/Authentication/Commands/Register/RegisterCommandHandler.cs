using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Localization;
using ErrorOr;
using Mediator;
using Microsoft.Extensions.Localization;
using BuberDinner.Domain.Common.SystemErrors;
using BuberDinner.Domain.Entities;
using BuberDinner.Application.Common.Interfaces.Repositories.Authentication;

namespace BuberDinner.Application.Services.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<UserResponse>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserQueryRepo _userQueryRepo;
        private readonly IUserCommandRepo _userCommandRepository;
        private readonly IStringLocalizer<ErrorLocalizer> _stringLocalizer;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator,
                                      IUserQueryRepo userQueryRepo,
                                      IUserCommandRepo userCommandRepository,
                                      IStringLocalizer<ErrorLocalizer> stringLocalizer)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userQueryRepo = userQueryRepo;
            _userCommandRepository = userCommandRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async ValueTask<ErrorOr<UserResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (_userQueryRepo.GetUserByEmail(request.Email) is not null)
                return Error.Conflict(_stringLocalizer[Errors.UserErrors.DuplicateEmailCode],
                                      _stringLocalizer[Errors.UserErrors.DuplicateEmailDescription]);

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
            };

            _userCommandRepository.AddUser(user);

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
