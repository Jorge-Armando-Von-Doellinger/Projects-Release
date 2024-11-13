using HMS.Notification.gRPC.Settings;
using HMS.Notification.Infrastructure.Settings;

namespace HMS.Notification.gRPC.Modules;

public static class SettingsModule
{
    internal static IServiceCollection AddSettingsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddgRpcSettings(configuration)
            .AddInfrastructureSettings(configuration);
        return services;
    }

    private static IServiceCollection AddgRpcSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration);
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