using HMS.ContractsMicroService.Infrastructure.Exceptions;
using HMS.ContractsMicroService.Infrastructure.Settings;
using HMS.ContractsMicroService.Infrastructure.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace HMS.ContractsMicroService.Infrastructure.Modules
{
    public static class SettingsModule
    {
        public static IServiceCollection AddInfrastructureSettingsModule(this IServiceCollection services, JsonElement jsonSettings)
        {
            services
                .AddDatabaseSettings(jsonSettings)
                .AddServiceDiscovery(jsonSettings);
            return services;
        }
        private static IServiceCollection AddDatabaseSettings(this IServiceCollection services, JsonElement jsonSettings)
        {

            if(jsonSettings.TryGetProperty("MongoDb", out var json))
            {
                Console.WriteLine(json);
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<DatabaseSettings>(json, jsonOptions) 
                    ?? throw new InvalidSettingsException("Database settings can't be setted");
                services.AddSingleton<IDatabaseSettings>(data);
            } else throw new Exception("Erro ao encontrar propertyName no json de configuração!");
            return services;
        }
        private static IServiceCollection AddServiceDiscovery(this IServiceCollection services, JsonElement jsonSettings)
        {
            if (jsonSettings.TryGetProperty("Consul", out var json))
            {
                Console.WriteLine(json);
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<ServiceDiscoverySettings>(json, jsonOptions)
                    ?? throw new InvalidSettingsException("Database settings can't be setted");
                services.AddSingleton<IServiceDiscoverySettings>(data);
            }
            else throw new Exception("Erro ao encontrar propertyName no json de configuração!");
            return services;
        }
    }
}
