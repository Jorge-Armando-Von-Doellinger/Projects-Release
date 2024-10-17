using HMS.ContractsMicroService.Messaging.Settings;
using HMS.ContractsMicroService.Messaging.Settings.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace HMS.ContractsMicroService.Messaging
{
    public static class SettingsModule
    {
        public static IServiceCollection AddMesssagingSettings(this IServiceCollection services, string configsJson)
        {
            services
                .AddMessagingComponents(configsJson)
                .AddMessagingSystem(configsJson)
                .AddMesssagingSettings(configsJson);
            return services;
        }

        private static IServiceCollection AddMessagingComponents(this IServiceCollection services, string json)
        {
            services.AddSingleton<IMessagingComponents>(sp =>
            {
                var data = JsonSerializer.Deserialize<MessagingComponents>(json);
                return data;
            });
            return services;
        }
        private static IServiceCollection AddMessagingSystem(this IServiceCollection services, string json)
        {
            services.AddSingleton<IMessagingSystem>(JsonSerializer.Deserialize<MessagingSystem>(json));
            return services;
        }
        private static IServiceCollection AddMessagingSettings(this IServiceCollection services, string json)
        {
            services.AddSingleton<IMessagingSettings>((sp) =>
            {
                return JsonSerializer.Deserialize<MessagingSettings>(json);
            });
            return services;
        }
    }
}
