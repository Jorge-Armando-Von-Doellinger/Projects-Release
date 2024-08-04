using HMS.Application.Mapper;
using HMS.Application.Services;
using HMS.Core.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace HMS.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddRepositories()
                .AddServices()
                .AddMappers();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<DataService>();
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddScoped<ClientMapper>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, IClientRepository>();
            return services;
        }
    }
}
