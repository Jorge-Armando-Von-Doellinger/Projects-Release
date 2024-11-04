using HMS.Payments.API.Services.Background;
using HMS.Payments.API.Services.Startup;
using HMS.Payments.Infrastructure.Settings.Implementations;

namespace HMS.Payments.API.Modules
{
    internal static class ApiModule
    {
        internal static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services
                .AddServices();
            return services;
        }

        internal     static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHostedService<RegisterSchemas>();
            services.AddHostedService<RegisterApiService>();

            services.AddHostedService<MessageConsumerService>();
            return services;
        }
    }
}
