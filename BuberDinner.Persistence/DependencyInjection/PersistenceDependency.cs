using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Repositories;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Persistence.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure.DependencyInjection
{
    public static class PersistenceDependency
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
