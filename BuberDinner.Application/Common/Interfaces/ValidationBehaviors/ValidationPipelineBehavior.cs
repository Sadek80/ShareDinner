using ErrorOr;
using FluentValidation;
using Mediator;

namespace BuberDinner.Application.Common.Interfaces.ValidationBehaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> :
                 IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
                                                        where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationPipelineBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async ValueTask<TResponse> Handle(TRequest message,
                                                 CancellationToken cancellationToken,
                                                 MessageHandlerDelegate<TRequest, TResponse> next)
        {
            if (_validator is null)
            {
                return await next(message, cancellationToken);
            }

            var validationResult = await _validator.ValidateAsync(message, cancellationToken);

            if (validationResult.IsValid)
            {
                return await next(message, cancellationToken);
            }

            var errors = validationResult.Errors
                                         .ConvertAll(validationFailure =>
                                                     Error.Validation(validationFailure.PropertyName,
                                                                      validationFailure.ErrorMessage));

            return (dynamic)errors;
        }
    }
}
