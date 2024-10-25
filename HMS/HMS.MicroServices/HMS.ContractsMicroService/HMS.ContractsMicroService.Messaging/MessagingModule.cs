using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Messaging.Connect;
using HMS.ContractsMicroService.Messaging.Listener;
using HMS.ContractsMicroService.Messaging.Publisher;
using HMS.ContractsMicroService.Messaging.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.ContractsMicroService.Messaging
{
    public static class MessagingModule
    {
        public static IServiceCollection AddMessagingModule(this IServiceCollection services)
        {
            services
                .AddConnections()
                .AddMessaging()
                .AddEvents();
            return services;
        }

        private static IServiceCollection AddConnections(this IServiceCollection services)
        {
            services.AddSingleton<ConnectMessaging>();
            return services;
        }

        private static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageListener, MessageListener>();
            services.AddScoped<IMessagePublisher, MessagePubisher>();
            return services;
        }

        private static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddSingleton<OnUpdatedSettings, MessagingSystem>();
            services.AddSingleton<OnUpdatedSettings, MessagingSettings>();
            return services;
        }
    }
}
