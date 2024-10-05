using HMS.ContractsMicroService.Messaging.Connect;
using HMS.ContractsMicroService.Messaging.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.ContractsMicroService.Messaging
{
    public static class MessagingModule
    {
        public static IServiceCollection AddMessagingModule(this IServiceCollection services)
        {
            services.AddScoped<CacheSettingsService>();
            services.AddScoped<ConnectMessaging>();
            return services;
        }
    }
}
