using HMS.Employee.Application.Manager;
using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Interface.Manager;
using HMS.Employee.Core.Interface.Repository;
using HMS.Employee.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Employee.Inputs;

namespace HMS.Employee.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddRepositories();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Core.Entity.Employee>, EmployeeRepository>();
            services.AddScoped<IRepositoryWithEmployeeId<Payroll>, PayrollRepository>();
            services.AddScoped<IRepositoryWithEmployeeId<ContractualInformation>, ContractsRepository>();
            return services;
        }
    }
}
