using Nuget.Settings.Messaging;
using Nuget.Settings.ServiceDiscovery;

namespace Nuget.Settings
{
    public sealed class AppSettings
    {
        public static AppSettings? CurrentSettings { get; internal set; } = null;
        public RabbitMqSettings? RabbitMq { get; set; }
        public ConsulSettings? Consul { get; set; }

        public void SetCurrentSettings(AppSettings settings)
        {
            CurrentSettings = settings;
        }
    }
}
