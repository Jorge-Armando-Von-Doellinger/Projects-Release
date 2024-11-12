using HMS.Notification.API.Services.HostedServices;

namespace HMS.Notification.API.Modules;

public static class gRpcModule
{
    internal static IServiceCollection AddApiModule(this IServiceCollection services)
    {
        services
            .AddServices();
        return services;    
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        //services.AddHostedService<ServiceDiscoveryRegistration>();
        return services;
    }
}