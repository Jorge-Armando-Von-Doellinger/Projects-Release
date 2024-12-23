﻿using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Messaging.Context;
using HMS.Payments.Messaging.Events;
using HMS.Payments.Messaging.Factory;
using HMS.Payments.Messaging.Listeners;
using HMS.Payments.Messaging.Publisher;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Payments.Messaging.Modules;

public static class MessagingModule
{
    public static IServiceCollection AddMessagingModule(this IServiceCollection services)
    {
        services
            .AddFactory()
            .AddContexts()
            .AddListener()
            .AddPublisher()
            .AddBackgroundServices();
        return services;
    }

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<MessageListener>();
        return services;
    }

    private static IServiceCollection AddFactory(this IServiceCollection services)
    {
        services.AddSingleton<ChannelFactory>();
        return services;
    }

    private static IServiceCollection AddContexts(this IServiceCollection services)
    {
        services.AddSingleton<RabbitContext>();
        return services;
    }

    private static IServiceCollection AddPublisher(this IServiceCollection services)
    {
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        return services;
    }

    private static IServiceCollection AddListener(this IServiceCollection services)
    {
        services.AddSingleton<IMessageListener, PaymentListener>();
        services.AddSingleton<IMessageListener, PaymentEmployeeListener>();
        return services;
    }

    private static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddSingleton<MessageEvents>();
        return services;
    }
}