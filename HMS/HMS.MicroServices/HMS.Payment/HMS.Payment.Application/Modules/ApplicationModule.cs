using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Manager;
using HMS.Payments.Application.Mapper;
using HMS.Payments.Application.Services;
using HMS.Payments.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Payments.Application.Modules
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddManagers()
                .AddServices()
                .AddMappers();
            return services;
        }
        private static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IEmployeePaymentManager, EmployeePaymentManager>();
            services.AddScoped<IPaymentManager, PaymentManager>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();
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
