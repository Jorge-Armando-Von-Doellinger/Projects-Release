using Microsoft.Extensions.DependencyInjection;

namespace HMS.ContractsMicroService.Messaging
{
    public static class MessagingModule
    {
        public static IServiceCollection AddMessagingModule(this IServiceCollection services)
        {
            return services;
        }
    }
}
