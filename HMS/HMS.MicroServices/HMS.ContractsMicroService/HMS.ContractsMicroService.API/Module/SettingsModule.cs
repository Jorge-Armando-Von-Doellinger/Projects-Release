using Consul;
using HMS.ContractsMicroService.API.Services.Data;
using HMS.ContractsMicroService.API.Settings;
using HMS.ContractsMicroService.API.Settings.Database;
using HMS.ContractsMicroService.API.Settings.Messaging;
using HMS.ContractsMicroService.API.Settings.ServiceDiscovery;
using HMS.ContractsMicroService.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nuget.Settings;
using Nuget.Settings.Database;
using Nuget.Settings.Messaging;
using Nuget.Settings.ServiceDiscovery;

namespace HMS.ContractsMicroService.API.Module
{
    public static class SettingsModule
    {
        internal static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSetting(configuration);
            return services;
        }

        private static IServiceCollection AddSetting(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("DefaultAppSettings").Get<AlternativeAppSettings>();
            services.AddSingleton<AlternativeAppSettings>();
            services.AddSingleton<IAppSettings>(sp =>
            {
                var settings = sp.GetRequiredService<AlternativeAppSettings>();
                return new AppSettings() // Erro aqui
                {
                    ApplicationName = settings.ApplicationName,
                    Consul = settings.Consul,
                    MessagingSystem = settings.MessagingSystem.ToDictionary(kvp => kvp.Key, kvp => (IMessagingSystem)kvp.Value),
                    MongoDb = settings.MongoDb,
                    RabbitMq = settings.RabbitMq
                };
            });
            services.AddSingleton<Dictionary<string, IMessagingSystem>>(sp =>
            {
                return sp.GetRequiredService<IAppSettings>().MessagingSystem;
                //return settings.MessagingSystem.ToDictionary(kvp => kvp.Key, kvp => (IMessagingSystem)kvp.Value); ;
            });
            services.AddSingleton<IRabbitMqSettings>(sp => sp.GetRequiredService<IAppSettings>().RabbitMq);
            services.AddSingleton<IMongoDbSettings>(sp => sp.GetRequiredService<IAppSettings>().MongoDb);
            services.AddSingleton<IConsulSettings>(sp => sp.GetRequiredService<IAppSettings>().Consul);
            SettingsStartupState.SetSettingsCompleted();
            return services;
        }
    }
}
