using HMS.Notification.AppConsole.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Notification.AppConsole.Module;

public static class TestsModule
{
    internal static IServiceCollection AddTestServices(this IServiceCollection services)
    {
        services.AddTestPublish();
        return services;    
    }

    private static IServiceCollection AddTestPublish(this IServiceCollection services)
    {
        services.AddScoped<PublishNotificationTest>();
        return services;
    }
}