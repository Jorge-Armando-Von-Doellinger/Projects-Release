using HMS.Notification.Core.Interfaces.Repository;
using HMS.Notification.Core.Interfaces.ServiceDiscovery;
using HMS.Notification.Infrastructure.Context;
using HMS.Notification.Infrastructure.Repository;
using HMS.Notification.Infrastructure.ServiceDiscovery;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace HMS.Notification.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        services
            .AddServiceDiscovery()
            .AddClient()
            .AddContext()
            .AddRepository();
        return services;
    }

    private static IServiceCollection AddClient(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb://localhost:27017"));
        return services;
    }
    private static IServiceCollection AddContext(this IServiceCollection services)
    {
        services.AddSingleton<MongoContext>();
        return services;
    }
    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<INotificationRepository, NotificationRepository>();
        return services;    
    }

    private static IServiceCollection AddServiceDiscovery(this IServiceCollection services)
    {
        services.AddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();
        return services;
    }
}