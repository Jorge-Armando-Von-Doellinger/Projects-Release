using Nuget.Settings.Database;
using Nuget.Settings.Messaging;
using Nuget.Settings.ServiceDiscovery;

namespace Nuget.Settings
{
    public sealed class AppSettings
    {
        public static AppSettings? CurrentSettings { get; private set; } = null;
        public RabbitMqSettings? RabbitMq { get; set; }
        public ConsulSettings? Consul { get; set; }
        public MongoDbSettings? MongoDb { get; set; }

        public static void SetCurrentSettings(AppSettings settings)
        {
            CurrentSettings = settings;
        }
    }
}
