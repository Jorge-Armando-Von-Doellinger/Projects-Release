using HMS.PayrollMicroService.Core.Repository;
using HMS.PayrollMicroService.Infratructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.PayrollMicroService.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddRepository();
            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IPayrollRepository, PayrollRepository>();
            return services;
        }
    }
}
