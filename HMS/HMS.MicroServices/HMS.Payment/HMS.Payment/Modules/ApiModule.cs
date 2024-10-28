﻿using HMS.Payments.API.Services.Startup;

namespace HMS.Payments.API.Modules
{
    internal static class ApiModule
    {
        internal static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services
                .AddServices();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHostedService<RegisterApiService>();
            return services;
        }
    }
}
