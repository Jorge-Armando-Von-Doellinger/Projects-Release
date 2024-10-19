using HMS.ContractsMicroService.Messaging.Exceptions;
using HMS.ContractsMicroService.Messaging.Settings.Interfaces;
using RabbitMQ.Client;

namespace HMS.ContractsMicroService.Messaging.Connect
{
    public sealed class ConnectMessaging
    {
        private readonly IMessagingSettings _settings;

        public ConnectMessaging(IMessagingSettings settings)
        {
            _settings = settings;
        }

        public IModel Connect()
        {
            if (_settings == null) throw new SettingsNotFoundedException("Configurações para conexão não encontradas!");
            Console.WriteLine(_settings.HostName + " Hostname");
            var factory = new ConnectionFactory()
            {
                HostName = _settings.HostName,
                Port = _settings.Port
            };
            var connection = factory.CreateConnection();
            return connection.CreateModel();
        }
    }
}
