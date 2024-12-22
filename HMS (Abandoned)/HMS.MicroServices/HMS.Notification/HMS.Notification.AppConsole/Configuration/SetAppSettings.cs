using Microsoft.Extensions.Configuration;

namespace HMS.Notification.AppConsole.Configuration;

public sealed class SetAppSettings
{
    internal static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory + "../../../")
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}