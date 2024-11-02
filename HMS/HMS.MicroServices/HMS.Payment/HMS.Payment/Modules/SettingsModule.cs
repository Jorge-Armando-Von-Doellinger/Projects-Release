using Consul;
using HMS.Payments.Infrastructure.Settings.Implementations;
using HMS.Payments.Infrastructure.Settings.Interfaces;
using HMS.Payments.Messaging.Settings;

namespace HMS.Payments.API.Modules
{
    internal static class SettingsModule
    {
        internal static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .SetInfrastructureSettings(configuration)
                .SetMessagingSettings(configuration);
            return services;
        }

        private static IServiceCollection SetInfrastructureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));
            services.Configure<ServiceDiscoverySettigs>(configuration.GetSection("ServiceDiscovery"));
            return services;
        }

        private static IServiceCollection SetMessagingSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MessagingSettings>(configuration.GetSection("Messaging"));
            services.Configure<MessagingSystem>(configuration);
            return services;
        }
    }
}
