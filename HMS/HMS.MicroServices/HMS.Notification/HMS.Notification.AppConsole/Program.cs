using DotNetEnv;
using HMS.Notification.AppConsole.Configuration;
using HMS.Notification.AppConsole.Module;
using HMS.Notification.AppConsole.Tests;
using HMS.Notification.Application.Modules;
using HMS.Notification.Infrastructure;
using HMS.Notification.Messaging.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HMS.Notification.AppConsole;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Notification Service Initialized!");
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.Sources.Clear();
                config.SetBasePath(
                    AppDomain.CurrentDomain.BaseDirectory+"../../../");
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                services
                    .AddApplicationModule()
                    .AddInfrastructureModule()
                    .AddMessagingModule()
                    .AddTestServices();
                AppConfigurator.AddAppSettings(services, context.Configuration);
            })
            .Build();
        Env.Load(AppDomain.CurrentDomain.BaseDirectory+"../../../.env");
        var a = host.Services.GetRequiredService<PublishNotificationTest>();
        await a.Publish();
        await host.StartAsync();
        Console.ReadLine();
    }

    static async Task Test(IServiceProvider serviceProvider)
    {
        var testPublisher = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PublishNotificationTest>();
        await testPublisher.Publish();
    }
}