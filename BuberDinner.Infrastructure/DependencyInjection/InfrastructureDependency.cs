using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Repositories;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace BuberDinner.Infrastructure.DependencyInjection
{
    public static class InfrastructureDependency
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuth(configuration);

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }

        private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings._section, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                                               Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    });

            return services;
        }

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
