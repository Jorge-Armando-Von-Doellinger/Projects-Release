using HMS.Payments.Infrastructure.Settings.Implementations;
using HMS.Payments.Messaging.Settings;

namespace HMS.Payments.Grpc.Modules;

public static class SettingsModule
{
    internal static IServiceCollection AddSettingsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructureSettings(configuration)
            .AddMessagingSettings(configuration);
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
}