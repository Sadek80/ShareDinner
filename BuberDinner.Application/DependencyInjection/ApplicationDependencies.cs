using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application.DependencyInjection
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediator();

            return services;
        }
    }
}
