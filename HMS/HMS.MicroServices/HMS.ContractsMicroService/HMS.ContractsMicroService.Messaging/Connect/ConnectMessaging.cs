using HMS.ContractsMicroService.Messaging.Exceptions;
using HMS.ContractsMicroService.Messaging.Services;
using Nuget.Settings;
using Nuget.Settings.Messaging;
using RabbitMQ.Client;

namespace HMS.ContractsMicroService.Messaging.Connect
{
    public sealed class ConnectMessaging
    {
        private readonly IRabbitMqSettings _settings;

        public ConnectMessaging(IRabbitMqSettings settings)
        {
            _settings = settings;
        }

        public IModel Connect()
        {
            if (_settings == null) throw new SettingsNotFoundedException("Configurações para conexão não encontradas!");
            Console.WriteLine(_settings.HostName);
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
