using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Interface.Repository;
using HMS.Employee.Infrastructure.Context;
using HMS.Employee.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Employee.Infrastructure
{
    public static class InfrastuctureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddContexts()
                .AddRepositories();
            return services;
        }

        public static IServiceCollection AddContexts(this IServiceCollection services)
        {
            services.AddScoped<EmployeeContext>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Core.Entity.Employee>, EmployeeRepository>();
            return services;
        }
    }
}
