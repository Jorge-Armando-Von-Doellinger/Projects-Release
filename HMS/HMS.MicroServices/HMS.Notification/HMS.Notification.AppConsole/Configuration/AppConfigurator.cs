using HMS.Notification.Infrastructure;
using HMS.Notification.Infrastructure.Settings;
using HMS.Notification.Messaging.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Notification.AppConsole.Configuration;

public static class AppConfigurator
{
    internal static IServiceCollection AddAppSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        try
        {
            Console.WriteLine("Loading Application Configuration...");
            serviceCollection
                .AddInfrastructureSettings(configuration)
                .AddMessagingSettings(configuration);
            Console.WriteLine("Application Configuration Loaded!");
            return serviceCollection;
        }
        catch
        {
            Console.WriteLine("Failed to load App Settings.");
            return serviceCollection;
        }
    }
    private static IServiceCollection AddInfrastructureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetRequiredSection("Database"));
        services.Configure<ServiceDiscoverySettings>(configuration.GetRequiredSection("ServiceDiscovery"));
        return services;
    }
    private static IServiceCollection AddMessagingSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessagingSettings>(configuration.GetSection("Messaging"));
        return services;
    }
}