using Microsoft.Extensions.Hosting;

namespace OmniSphere.Inventory.Console;

class Program
{
    static async Task Main(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args);
        hostBuilder.ConfigureServices((hostContext, services) =>
        {
            
        });
        var host = hostBuilder.Build();
        await host.StartAsync();
        await host.RunAsync();
        await host.WaitForShutdownAsync();
    }
}