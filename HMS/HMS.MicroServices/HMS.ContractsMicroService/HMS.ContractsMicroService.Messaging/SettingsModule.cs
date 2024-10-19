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
            if (element.TryGetProperty("MessagingSystem", out var json))
            {
                Console.WriteLine(json);
                var components = JsonSerializer.Deserialize<Dictionary<string, MessagingComponents>>(json.GetRawText());
                data = new() { Components = components.ToDictionary(x => x.Key, x => (IMessagingComponents)x.Value) };
                foreach (var item in data.Components.Values)
                {
                    item.SetKeys();
                    foreach (var item1 in item.Keys)
                    {
                        Console.WriteLine(item1);
                    }
                }
            }
            else throw new Exception("Erro ao encontrar propertyName no json de configuração!");
            services.AddSingleton<IMessagingSystem>(data);
            return services;
        }
        private static IServiceCollection AddMessagingSettings(this IServiceCollection services, JsonElement element)
        {
            MessagingSettings? settings = null;
            if (element.TryGetProperty("RabbitMq", out var json))
            {
                Console.WriteLine(json);
                settings = JsonSerializer.Deserialize<MessagingSettings>(json.GetRawText());
                var data = JsonSerializer.Deserialize<MessagingSettings>(json.GetRawText());
                Console.WriteLine(data.Port == 0);
            }
            else throw new Exception("Erro ao encontrar propertyName no json de configuração!");
            services.AddSingleton<IMessagingSettings>(settings);
            return services;
        }
    }
}
