using HMS.ContractsMicroService.API.Services;
using HMS.ContractsMicroService.API.Settings;
using HMS.ContractsMicroService.API.Settings.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Modules;
using HMS.ContractsMicroService.Messaging;
using System.Text.Json;

namespace HMS.ContractsMicroService.API.Module
{
    public static class SettingsModule
    {
        private static AppSettingsService? _service = new();
        internal static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInfrastructureSettings(configuration)
                .AddMessagingSettngsModule(configuration)
                .AddApiSettings(configuration);
            return services;
        }

        private static IServiceCollection AddInfrastructureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var element = _service.GetSettings(configuration);
            services.AddInfrastructureSettingsModule(element);
            return services;
        }

        private static IServiceCollection AddMessagingSettngsModule(this IServiceCollection services, IConfiguration configuration)
        {
            var element = _service.GetSettings(configuration);
            services.AddMesssagingSettingsModule(element);
            return services;
        }
        private static IServiceCollection AddApiSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var element = _service.GetSettings(configuration);
            var options = new JsonSerializerOptions () { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<AppSettings>(element, options);
            services.AddSingleton<IAppSettings>(data);
            return services;
        }
    }
}
