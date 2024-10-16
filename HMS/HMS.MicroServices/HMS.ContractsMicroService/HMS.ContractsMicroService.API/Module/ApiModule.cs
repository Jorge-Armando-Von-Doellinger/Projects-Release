using HMS.ContractsMicroService.API.Services.Background;

namespace HMS.ContractsMicroService.API.Module
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services
                .AddHostedServices();
            return services;
        }



        private static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<MessageListenerService>();
            return services;
        }

    }
}
