using System.Reflection;
using HMS.Notification.Application.Interfaces;
using HMS.Notification.Application.Manager;
using HMS.Notification.Application.Mapper;
using HMS.Notification.Application.Processor;
using HMS.Notification.Application.Services;
using HMS.Notification.Core.Interfaces.Messaging;
using HMS.Notification.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Notification.Application.Modules;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddManagers()
            .AddMappers()
            .AddServices()
            .AddProcessors();
        return services;    
    }

    private static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddScoped<INotificationManager, NotificationManager>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
        return services;
    }

    private static IServiceCollection AddProcessors(this IServiceCollection services)
    {
        services.AddSingleton<IMessageProcessor, MessageProcessor>();
        return services;
    }
    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<NotificationMapper>();
        return services;
    }
}
