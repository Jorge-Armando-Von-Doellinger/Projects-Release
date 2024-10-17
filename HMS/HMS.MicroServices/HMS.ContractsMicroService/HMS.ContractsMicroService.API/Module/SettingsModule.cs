using HMS.ContractsMicroService.API.Services.Data;
using HMS.ContractsMicroService.API.Settings;
using HMS.ContractsMicroService.Messaging;
using Microsoft.VisualBasic;
using System.Text.Json;

namespace HMS.ContractsMicroService.API.Module
{
    public static class SettingsModule
    {
        internal static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMessagingSettngsModule(configuration);
            return services;
        }
        private static IServiceCollection AddMessagingSettngsModule(this IServiceCollection services, IConfiguration configuration)
        {
            var data = configuration.GetRequiredSection("DefaultAppSettings").Get<JsonElement>();
            Console.WriteLine(data.GetRawText());
            services.AddMesssagingSettings(data.GetRawText());
            return services;
        }
    }
}
