using HMS.Payments.Messaging.Settings;

namespace HMS.Payments.Grpc.Modules;

public static class SettingsModule
{
    internal static IServiceCollection AddSettingsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMessagingSettings(configuration);
        return services;
    }

    private static IServiceCollection AddMessagingSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MessagingSettings>(configuration);
        return services;
    }
}