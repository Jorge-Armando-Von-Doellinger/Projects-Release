using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Payments.Application.Modules
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddManagers();
            return services;
        }
        private static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IEmployeePaymentManager, EmployeePaymentManager>();
            services.AddScoped<IPaymentManager, PaymentManager>();
            return services;
        }
    }
}
