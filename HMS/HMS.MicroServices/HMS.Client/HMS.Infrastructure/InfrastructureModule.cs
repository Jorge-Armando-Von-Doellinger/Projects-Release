using HMS.Infrastructure.DataContext;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddContexts(this IServiceCollection services)
        {
            services.AddScoped<ClientContext>();
            return services;
        }
    }
}
