using Gateway.v1.Core.Messaging.Publisher;
using Gateway.v1.Messaging.Configurator;
using Gateway.v1.Messaging.Factory;
using Gateway.v1.Messaging.Publisher;
using Microsoft.Extensions.DependencyInjection;
using Nuget.MessagingUtilities.MessageSettings;

namespace Gateway.v1.Messaging
{
    public static class MessagingModule
    {
        public static IServiceCollection AddMessagingModule(this IServiceCollection services)
        {
            services
                .AddFactories()
                .AddConfigurators()
                .AddPublishers();
            return services;
        }

        public static IServiceCollection AddPublishers(this IServiceCollection services)
        {
            services.AddScoped<IMessagePublisher<IMessageSettings>, RabbitMqPublisher>();
            return services;
        }

        public static IServiceCollection AddFactories(this IServiceCollection services)
        {
            services.AddScoped<ChannelFactory>();
            return services;
        }

        public static IServiceCollection AddConfigurators(this  IServiceCollection services)
        {
            services.AddScoped<ChannelConfigurator>();
            return services;
        }

        
    }
}
