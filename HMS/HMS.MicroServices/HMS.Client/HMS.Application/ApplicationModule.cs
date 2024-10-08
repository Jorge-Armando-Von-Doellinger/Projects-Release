﻿using HMS.Application.Mapper;
using HMS.Application.Services;
using HMS.Core.Interfaces.Repository;
using HMS.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace HMS.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
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

    }
}
