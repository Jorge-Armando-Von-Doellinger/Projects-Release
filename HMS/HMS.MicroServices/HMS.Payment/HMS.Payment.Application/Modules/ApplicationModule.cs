using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Manager;
using HMS.Payments.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Payments.Application.Modules
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddManagers()
                .AddMappers();
            return services;
        }
        private static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IEmployeePaymentManager, EmployeePaymentManager>();
            services.AddScoped<IPaymentManager, PaymentManager>();
            return services;
        }
        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddScoped<EmployeePaymentMapper>();
            services.AddScoped<PaymentMapper>();
            return services;
        }
    }
}
