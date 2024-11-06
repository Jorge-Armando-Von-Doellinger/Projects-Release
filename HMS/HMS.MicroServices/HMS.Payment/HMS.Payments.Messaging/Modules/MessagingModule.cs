using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Messaging.Context;
using HMS.Payments.Messaging.Factory;
using HMS.Payments.Messaging.Listeners;
using HMS.Payments.Messaging.Publisher;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HMS.Payments.Messaging.Modules
{
    public static class MessagingModule
    {
        public static IServiceCollection AddMessagingModule(this IServiceCollection services)
        {
            services
                .AddFactory()
                .AddChannel()
                .AddContexts()
                .AddListener()
                .AddPublisher();
            return services;
        }

        private static IServiceCollection AddFactory(this IServiceCollection services)
        {
            services.AddSingleton<ChannelFactory>();
            return services;
        }

        private static IServiceCollection AddChannel(this IServiceCollection services)
        {
            services.AddSingleton<IModel>(sp =>
            {
                var factory = sp.GetRequiredService<ChannelFactory>();
                return factory.Channel;
            });
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
            services.AddSingleton<IMessageListener, MessageListener>();
            return services;
        }
    }
}
