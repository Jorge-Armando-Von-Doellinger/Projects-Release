using HMS.ContractsMicroService.Infrastructure.Exceptions;
using HMS.ContractsMicroService.Infrastructure.Settings;
using HMS.ContractsMicroService.Infrastructure.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace HMS.ContractsMicroService.Infrastructure.Modules
{
    public static class SettingsModule
    {
        public static IServiceCollection AddInfrastructureSettingsModule(this IServiceCollection services, string jsonSettings)
        {
            services
                .AddDatabaseSettings(jsonSettings);
            return services;
        }
        private static IServiceCollection AddDatabaseSettings(this IServiceCollection services, string jsonSettings)
        {
            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return JsonSerializer.Deserialize<DatabaseSettings>(jsonSettings) ?? throw new InvalidSettingsException("Database settings can't be setted");
            });
            return services;
        }
    }
}
