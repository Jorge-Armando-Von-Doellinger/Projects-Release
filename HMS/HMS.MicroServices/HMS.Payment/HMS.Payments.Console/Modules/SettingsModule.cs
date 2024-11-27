using HMS.Payments.External_Services.Settings;
using HMS.Payments.Infrastructure.Settings.Implementations;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Payments.Console.Modules;

public static class SettingsModule
{
    internal static IServiceCollection AddSettingsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructureSettings(configuration)
            .AddMessagingSettings(configuration)
            .AddExternalSettings(configuration);
        return services;
    }

    private static IServiceCollection AddInfrastructureSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection("Database"));
        return services;
    }
    private static IServiceCollection AddMessagingSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MessagingSettings>(configuration.GetSection("Messaging"));
        return services;
    }

    private static IServiceCollection AddExternalSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<NotificationSettings>(configuration.GetSection("External-Settings:Notification"));
        return services;
    }
}