using HMS.ContractsMicroService.Messaging.Services;
using Nuget.Settings;
using Nuget.Settings.Messaging;
using RabbitMQ.Client;

namespace HMS.ContractsMicroService.Messaging.Connect
{
    public sealed class ConnectMessaging
    {

        public ConnectMessaging()
        {
            
        }

        public IModel Connect()
        {
            if (AppSettings.CurrentSettings == null) throw new Exception("BVatata");
            
            var settings = AppSettings.CurrentSettings.RabbitMq;
            if(settings == null) throw new InvalidOperationException("Messaging settings not founded");
            var factory = new ConnectionFactory()
            {
                HostName = settings.HostName,
                Port = settings.Port       
            };
            var connection = factory.CreateConnection();
            return connection.CreateModel();
        }
    }
}
