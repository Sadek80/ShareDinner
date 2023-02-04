using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Repositories;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure.DependencyInjection
{
    public static class InfrastructureDependency
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings._section));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
