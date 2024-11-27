using HMS.Payments.Core.Interfaces.External_Services;
using HMS.Payments.External_Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Payments.External_Services.Modules;

public static class ExternalServicesModule
{
    public static IServiceCollection AddExternalServicesModule(this IServiceCollection services)
    {
        services
            .AddExternalServices();
        return services;
    }
    private static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationExternalService, NotificationExternalService>();
        return services;
    }
}