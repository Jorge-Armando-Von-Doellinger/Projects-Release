using HMS.Payments.Infratructure.Context;
using HMS.Payments.Infratructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Payments.Infrastructure
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
            services.AddTransient<EmployeePaymentContext>();
            return services;
        }
    }
}
