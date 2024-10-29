using HMS.Payments.Messaging.Factory;
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
                .AddChannel();
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

    }
}
