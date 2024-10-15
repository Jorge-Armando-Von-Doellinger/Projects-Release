using HMS.ContractsMicroService.Messaging.Exceptions;
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
            if (IAppSettings.CurrentSettings == null) throw new SettingsNotFoundedException("Configurações para conexão não encontradas!");
            
            var settings = IAppSettings.CurrentSettings.RabbitMq;
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
