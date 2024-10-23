using HMS.ContractsMicroService.Messaging.Settings;
using HMS.ContractsMicroService.Messaging.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace HMS.ContractsMicroService.Messaging
{
    public static class SettingsModule
    {
        public static IServiceCollection AddMesssagingSettingsModule(this IServiceCollection services, JsonElement configsJson)
        {
            services
                .AddMessagingSystem(configsJson)
                .AddMessagingSettings(configsJson);
            return services;
        }
        private static IServiceCollection AddMessagingSystem(this IServiceCollection services, JsonElement element)
        {
            MessagingSystem? data = null;
            if (element.TryGetProperty(Keys.MessagingSystemKey, out var json))
            {
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var components = JsonSerializer.Deserialize<Dictionary<string, MessagingComponents>>(json.GetRawText(), jsonOptions);
                components.Values.All(x =>
                {
                    x.SetKeys();
                    return true;
                });
                data = new() { Components = components.ToDictionary(x => x.Key, x => (IMessagingComponents)x.Value) };
                
            }
            else throw new Exception("Erro ao encontrar propertyName no json de configuração!");
            services.AddSingleton<IMessagingSystem>(data);
            return services;
        }
        private static IServiceCollection AddMessagingSettings(this IServiceCollection services, JsonElement element)
        {
            MessagingSettings? settings = null;
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            if (element.TryGetProperty(Keys.MessagingSettingsKey, out var json))
            {
                settings = JsonSerializer.Deserialize<MessagingSettings>(json.GetRawText(), jsonOptions);
            }
            else throw new Exception("Erro ao encontrar propertyName no json de configuração!");
            services.AddSingleton<IMessagingSettings>(settings);
            return services;
        }
    }
}
