using HMS.ContractsMicroService.API.Settings.Database;
using HMS.ContractsMicroService.API.Settings.Messaging;
using HMS.ContractsMicroService.API.Settings.ServiceDiscovery;
using Nuget.Settings;
using Nuget.Settings.Database;
using Nuget.Settings.Messaging;
using Nuget.Settings.ServiceDiscovery;

namespace HMS.ContractsMicroService.API.Settings
{
    public class AlternativeAppSettings
    {
        public MessagingSettings? RabbitMq { get; set; }    

        public ServiceDiscoverySettings? Consul { get; set; }

        public DatabaseSettings? MongoDb { get; set; }

        public Dictionary<string, MessagingSystem> MessagingSystem { get; set; }

        public string ApplicationName {  get; set; }

    }
}
