using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Messaging.Connect;
using HMS.ContractsMicroService.Messaging.Listener;
using HMS.ContractsMicroService.Messaging.Publisher;
using HMS.ContractsMicroService.Messaging.Services;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Settings.Messaging;

namespace HMS.ContractsMicroService.Messaging
{
    public static class MessagingModule
    {
        public static IServiceCollection AddMessagingModule(this IServiceCollection services)
        {
            services
                .AddConnections()
                .AddMessaging()
                .AddServices();
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
            services.AddScoped<IMessagePublisher<RabbitMqSettings>, MessagePubisher>();
            return services;
        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<CacheSettingsService>();
            return services;
        }
    }
}
