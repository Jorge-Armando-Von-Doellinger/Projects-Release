using Consul;
using HMS.ContractsMicroService.Infrastructure.Modules;
using HMS.ContractsMicroService.Messaging;
using System.Text.Json;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMS.ContractsMicroService.API.Module
{
    public static class SettingsModule
    {
        internal static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInfrastructureSettings(configuration)
                .AddMessagingSettngsModule(configuration);
            return services;
        }

        private static JsonElement TryGetJsonElemt(IConfigurationSection section)
        {
            var dict = new Dictionary<string, object>();

            foreach (var child in section.GetChildren())
            {
                if (child.GetChildren().Any())
                    dict[child.Key] = TryGetJsonElemt(child);
                else
                {
                    try
                    {
                        var value = Convert.ToInt32(child.Value);
                        dict[child.Key] = value;
                        continue;
                    }
                    catch { }
                    dict[child.Key] = child.Value; 
                }
            }
            var jsonString = JsonSerializer.Serialize(dict, new JsonSerializerOptions { WriteIndented = true });
            var jsonDoc = JsonDocument.Parse(jsonString);
            return jsonDoc.RootElement;
        }

        private static JsonElement GetSettings(IConfiguration configuration)
        {
            var section = configuration.GetSection("DefaultAppSettings");
            var jsonElement = TryGetJsonElemt(section);
            return jsonElement;
        }

        private static IServiceCollection AddInfrastructureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureSettingsModule(GetSettings(configuration).GetRawText());
            return services;
        }

        private static IServiceCollection AddMessagingSettngsModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMesssagingSettingsModule(GetSettings(configuration));
            return services;
        }
    }
}
