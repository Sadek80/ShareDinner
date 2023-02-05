using BuberDinner.Application.Common.Interfaces.Repositories.Authentication;
using BuberDinner.Persistence.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure.DependencyInjection
{
    public static class PersistenceDependency
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddTransient<IUserCommandRepo, UserCommandRepo>();
            services.AddTransient<IUserQueryRepo, UserQueryRepo>();

            return services;
        }
    }
}
