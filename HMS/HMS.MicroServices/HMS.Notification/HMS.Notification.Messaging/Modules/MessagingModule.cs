using HMS.Notification.Core.Interfaces.Messaging;
using HMS.Notification.Messaging.Factory;
using HMS.Notification.Messaging.Listener;
using HMS.Notification.Messaging.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Notification.Messaging.Modules;

public static class MessagingModule
{
    public static IServiceCollection AddMessagingModule(this IServiceCollection services)
    {
        services
            .AddFactories()
            .AddListeners()
            .AddPublishers()
            .AddBackgroundServices();
        return services;
    }

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<RabbitMqListener>();
        return services;
    }

    private static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddSingleton<ChannelFactory>();
        return services;
    }
    private static IServiceCollection AddListeners(this IServiceCollection services)
    {
        services.AddSingleton<IMessageListener, RabbitMqListener>();
        return services;
    }
    private static IServiceCollection AddPublishers(this IServiceCollection services)
    {
        services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
        return services;
    }
}