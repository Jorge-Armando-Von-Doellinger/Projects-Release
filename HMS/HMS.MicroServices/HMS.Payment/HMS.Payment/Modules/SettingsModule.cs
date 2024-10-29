using Consul;
using HMS.Payments.Infrastructure.Settings.Implementations;
using HMS.Payments.Infrastructure.Settings.Interfaces;

namespace HMS.Payments.API.Modules
{
    internal static class SettingsModule
    {
        internal static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .SetInfrastructureSettings(configuration);
            return services;
        }

        private static IServiceCollection SetInfrastructureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));
            services.Configure<ServiceDiscoverySettigs>(configuration.GetSection("ServiceDiscovery"));
            return services;
        }
    }
}
