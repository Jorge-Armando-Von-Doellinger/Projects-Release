using HMS.Notification.gRPC.Mapper;
using HMS.Notification.gRPC.Services.HostedServices;

namespace HMS.Notification.gRPC.Modules;

public static class gRpcModule
{
    internal static IServiceCollection AddgRpcModule(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddMappers();
        return services;    
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHostedService<ServiceDiscoveryRegistration>();
        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<ProtoDtoMapper>();
        return services;
    } 
}