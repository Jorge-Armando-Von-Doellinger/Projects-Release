using HMS.Payments.Application.Modules;
using HMS.Payments.Console.Modules;
using HMS.Payments.External_Services.Modules;
using HMS.Payments.Infrastructure.Modules;
using HMS.Payments.Messaging.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HMS.Payments.Console;

class Program
{
    static async Task Main(string[] args)
    {
        var host = Host
            .CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.Sources.Clear();
                config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory+"../../../"); // Volta 3 diretorios
                config.AddJsonFile("appsettings.json", false, true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                services
                    .AddSettingsModule(context.Configuration)
                    .AddApplicationModule()
                    .AddInfrastructureModule()
                    .AddMessagingModule()
                    .AddExternalServicesModule();
                services.AddScoped<TestClass>();
            })
            .Build();
        
        await host.StartAsync();
        var test = host.Services.GetService<TestClass>();
        await test.RunAsync();
        //await host.WaitForShutdownAsync();
        System.Console.ReadKey();
    }
}