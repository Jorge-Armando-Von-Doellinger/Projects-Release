using Gateway.v1.Application.Facades;
using Gateway.v1.Application.Managers;
using Gateway.v1.Application.Request;
using Gateway.v1.Application.Services.Clients;
using Gateway.v1.Application.Services.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Client.MessagingSettings;

namespace Gateway.v1.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddManagers()
                .AddServices()
                .AddFacades()
                .AddSettings()
                .AddTests();
            return services;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<ClientManager>();
            return services;
        }

        public static IServiceCollection AddFacades(this IServiceCollection services)
        {
            services.AddScoped<ClientFacade>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<GetClientsService>();
            services.AddScoped<MessageService>();
            return services;
        }

        public static IServiceCollection AddSettings(this  IServiceCollection services)
        {
            services.AddScoped<ClientMessagingSettings>();
            return services;
        }

        public static IServiceCollection AddTests(this IServiceCollection services)
        {
            return services;
        }
    }
}
