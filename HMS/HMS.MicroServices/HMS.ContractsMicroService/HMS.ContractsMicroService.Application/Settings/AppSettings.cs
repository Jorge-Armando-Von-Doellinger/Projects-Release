using HMS.ContractsMicroService.Application.Enums;
using Nuget.Settings;
using Nuget.Settings.Database;
using Nuget.Settings.Messaging;
using Nuget.Settings.ServiceDiscovery;
using System.Runtime.CompilerServices;

namespace HMS.ContractsMicroService.Application.Settings
{
    public class AppSettings : IAppSettings
    {
        public IRabbitMqSettings? RabbitMq { get; set; }

        public ConsulSettings? Consul { get; set; }

        public IMongoDbSettings? MongoDb { get; set; }

        public string ApplicationName { get; set; }

        public Dictionary<string, IMessagingSystem> MessagingSystem { get; set; }

        public static void SetCurrentSettings(IAppSettings settings)
        {
            IAppSettings.CurrentSettings = settings;
        }
    }
    public static class AppSettingsExtensions
    {
        public static IMessagingSystem GetMessagingSystem(this IAppSettings settings, MessagingSystemKeysEnum key)
        {
            if (settings == null) throw new NullReferenceException("Settings can't be null");
            bool success = settings.MessagingSystem.TryGetValue(key.ToString(), out var messagingSystem);
            return success ? messagingSystem : null;

        }
    }
}
