using BuberDinner.Application.Services.Implementations.Authentication;
using BuberDinner.Application.Services.Interfaces.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application.DependencyInjection
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
