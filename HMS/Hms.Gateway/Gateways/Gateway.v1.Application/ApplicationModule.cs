using Gateway.v1.Application.Enums;
using Gateway.v1.Application.Facades;
using Gateway.v1.Application.Factories;
using Gateway.v1.Application.Interfaces;
using Gateway.v1.Application.Managers;
using Gateway.v1.Application.Request;
using Gateway.v1.Application.Services.Clients;
using Gateway.v1.Application.Services.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Client.MessagingSettings;
using System.Security.Cryptography;

namespace Gateway.v1.Application
{
    public static class ApplicationModule
    {
        private static ManagerFactory B { get; set; }
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddFactories()
                .AddManagers()
                .AddServices()
                .AddFacades()
                .AddSettings()
                .AddTests();
            return services;
        }
        private static ManagerFactory Teste(ManagerFactory managerFactory)
        {
            B = managerFactory;
            return managerFactory;
        }

        public static IServiceCollection AddFactories(this IServiceCollection services)
        {
            services.AddTransient<ManagerFactory>();
            return services;
        }
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IManager, ClientManager>();
            services.AddScoped<ClientManager>();
            services.AddScoped<Func<Enum, IManager>>(serviceProvider => (key) =>
            {
                IManager manager;
                switch (key)
                {
                    case ManageFactoryEnums.ClientManager:
                        manager = (IManager) serviceProvider.GetRequiredService(typeof(ClientManager));
                        break;
                    default:
                        throw new Exception("Enum incorreto! ManagerFactory-GetManager!");
                }
                return manager;
            });
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
