using HMS.PayrollMicroService.Infratructure.Context;
using HMS.PayrollMicroService.Infratructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.PayrollMicroService.Infratructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddContext()
                .AddServices();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<TransactionService>();
            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services)
        {
            services.AddTransient<PayrollContext>();
            return services;
        }
    }
}
