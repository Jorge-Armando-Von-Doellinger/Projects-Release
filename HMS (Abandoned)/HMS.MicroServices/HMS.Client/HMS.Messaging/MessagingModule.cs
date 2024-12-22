using HMS.Messaging.Configurators;
using HMS.Messaging.Factorys;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Messaging
{
    public static class MessagingModule
    {
        public static IServiceCollection AddMessagingModule(this IServiceCollection services)
        {
            services
                .AddFactorys()
                .AddConfigurator();
            return services;
        }

        public static IServiceCollection AddFactorys(this IServiceCollection services)
        {
            services.AddSingleton<ClientChannelFactory>();
            return services;
        }
        public static IServiceCollection AddConfigurator(this IServiceCollection services)
        {
            services.AddSingleton<ClientChannelConfigurator>();
            return services;
        }
    }
}
