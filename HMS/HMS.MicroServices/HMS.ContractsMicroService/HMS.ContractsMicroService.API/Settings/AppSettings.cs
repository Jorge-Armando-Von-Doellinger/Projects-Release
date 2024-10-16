using HMS.ContractsMicroService.Application.Enums;
using Nuget.Settings;
using Nuget.Settings.Database;
using Nuget.Settings.Messaging;
using Nuget.Settings.ServiceDiscovery;

namespace HMS.ContractsMicroService.API.Settings
{
    public class AppSettings : IAppSettings
    {
        public static IAppSettings CurrentSettings { get; private set; } 
        public IRabbitMqSettings? RabbitMq { get; set; }
        public IConsulSettings? Consul { get; set; }

        public IMongoDbSettings? MongoDb { get; set; }

        public string ApplicationName { get; set; }

        public Dictionary<string, IMessagingSystem> MessagingSystem { get; set; }

        public void SetCurrentSettings(IAppSettings settings)
        {
            CurrentSettings = settings;
        }

        public void SetCurrentSettings()
        {
            
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
