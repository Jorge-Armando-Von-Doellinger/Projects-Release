using HMS.ContractsMicroService.API.Services;
using HMS.ContractsMicroService.API.Services.Background;
using HMS.ContractsMicroService.API.Services.Hosted;

namespace HMS.ContractsMicroService.API.Module
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services
                .AddHostedServices()
                .AddServices();
            return services;
        }


        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<AppSettingsService>();
            return services;
        }


        private static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<MessageListenerService>();
            services.AddHostedService<RegisterDtosSchemas>();
            services.AddHostedService<RegisterAppSettings>();
            return services;
        }

    }
}
