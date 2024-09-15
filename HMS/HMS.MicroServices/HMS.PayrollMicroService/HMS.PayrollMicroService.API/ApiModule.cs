using HMS.PayrollMicroService.Application.Interfaces.Manager;
using HMS.PayrollMicroService.Application.Manager;

namespace HMS.PayrollMicroService.API
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services
                .AddManagers();
            return services;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddTransient<IPayrollManager, PayrollManager>();
            return services;
        }
    }
}
