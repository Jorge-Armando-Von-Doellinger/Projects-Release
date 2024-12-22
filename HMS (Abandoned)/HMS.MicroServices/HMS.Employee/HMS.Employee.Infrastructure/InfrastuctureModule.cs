using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Interface.Repository;
using HMS.Employee.Infrastructure.Context;
using HMS.Employee.Infrastructure.DataContext;
using HMS.Employee.Infrastructure.Repository;
using HMS.Employee.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Employee.Infrastructure
{
    public static class InfrastuctureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddContexts()
                .AddServices();
            return services;
        }

        public static IServiceCollection AddContexts(this IServiceCollection services)
        {
            services.AddScoped<DefaultContext>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<TransactionService>();
            return services;
        }


    }
}
