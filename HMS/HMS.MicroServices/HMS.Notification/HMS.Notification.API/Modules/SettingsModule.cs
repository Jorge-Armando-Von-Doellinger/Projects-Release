using HMS.Notification.API.Settings;
using HMS.Notification.Infrastructure;
using HMS.Notification.Infrastructure.Settings;

namespace HMS.Notification.API.Modules;

public static class SettingsModule
{
    internal static IServiceCollection AddSettingsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor() // Native
            .AddApiSettings(configuration)
            .AddInfrastructureSettings(configuration);
        return services;
    }

    private static IServiceCollection AddApiSettings(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.Configure<ApiSettings>(configuration);
        return services;
    }
    private static IServiceCollection AddInfrastructureSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetRequiredSection("Database"));
        services.Configure<ServiceDiscoverySettings>(configuration.GetRequiredSection("ServiceDiscovery"));
        return services;
    } 
}