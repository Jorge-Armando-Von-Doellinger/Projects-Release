using HMS.Payments.Infrastructure.Settings.Implementations;
using HMS.Payments.Infrastructure.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Payments.Infrastructure.Modules
{
    public static class SettingsModule
    {
        public static IServiceCollection AddInfrastructureSettings(this IServiceCollection services, string json    )
        {
            return services;
        }

        private static IServiceCollection AddDatabaseSettings(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseSettings, DatabaseSettings>();
            return services;
        }
    }
}
