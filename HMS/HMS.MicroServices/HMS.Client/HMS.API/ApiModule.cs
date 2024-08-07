using HMS.Application.Managers;

namespace HMS.API
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
            services.AddScoped<ClientManager>();
            return services;
        }   
    }
}
