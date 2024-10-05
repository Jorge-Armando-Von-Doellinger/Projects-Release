using Nuget.Settings.Messaging;
using Nuget.Settings.ServiceDiscovery;

namespace Nuget.Settings
{
    public sealed class AppSettings
    {
        public RabbitMqSettings RabbitMq { get; set; }
        public ConsulSettings? Consul { get; set; }
    }
}
