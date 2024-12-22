using HMS.Application.Managers;
using HMS.Application.MessageProcessor;
using HMS.Core.Interfaces.Messaging;
using HMS.Messaging.Listeners;
using HMS.Messaging.Publishers;
using Nuget.MessagingUtilities;

namespace HMS.API
{
    public static class MessageModuleExtension
    {
        public static IServiceCollection AddMessagingModuleExtension(this IServiceCollection services)
        {
            services
                .AddProcessors()
                .AddMessageManagers()
                .AddManagers()
                .AddMessageListener()
                .AddMessagePublisher();
            return services;
        }

        public static IServiceCollection AddMessagePublisher(this IServiceCollection services)
        {
            services.AddSingleton<IMessagePublisher, RabbitMessagePublisher>();
            return services;
        }

        public static IServiceCollection AddMessageListener(this IServiceCollection services)
        {
            services.AddHostedService<RabbitMessageListener>();
            return services;
        }

        public static IServiceCollection AddProcessors(this IServiceCollection services)
        {
            services.AddScoped<IMessageProcessor<Message>, MessageProcessor>();
            return services;
        }

        public static IServiceCollection AddMessageManagers(this IServiceCollection services)
        {
            services.AddScoped<ClientMessageManager>();
            return services;
        }
    }
}
