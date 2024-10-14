using Nuget.Settings.Database;
using Nuget.Settings.Messaging;
using Nuget.Settings.ServiceDiscovery;

namespace Nuget.Settings
{
    public interface IAppSettings
    {
        static IAppSettings? CurrentSettings { get; protected set; } = null;
        IRabbitMqSettings? RabbitMq { get; }
        ConsulSettings? Consul { get; }
        IMongoDbSettings? MongoDb { get; }
        Dictionary<string, IMessagingSystem> MessagingSystem { get; }
        string ApplicationName { get; }
    }
}
