using BuberDinner.Application.Common.Interfaces.ValidationBehaviors;
using BuberDinner.Application.Services.Authentication.Commands.Register;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuberDinner.Application.DependencyInjection
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediator();

            services.AddSingleton(typeof(IPipelineBehavior<,>),
                                  typeof(ValidationPipelineBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
