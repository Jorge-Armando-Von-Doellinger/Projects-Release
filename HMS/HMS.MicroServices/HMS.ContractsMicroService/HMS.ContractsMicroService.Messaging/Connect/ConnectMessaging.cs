using HMS.ContractsMicroService.Messaging.Exceptions;
using HMS.ContractsMicroService.Messaging.Services;
using RabbitMQ.Client;

namespace HMS.ContractsMicroService.Messaging.Connect
{
    public sealed class ConnectMessaging
    {

        public ConnectMessaging(object settings)
        {
            //_settings = settings;
        }

        public IModel Connect()
        {
            /*if (_settings == null) throw new SettingsNotFoundedException("Configurações para conexão não encontradas!");
            Console.WriteLine(_settings.HostName);
            var factory = new ConnectionFactory()
            {
                HostName = _settings.HostName,
                Port = _settings.Port       
            };
            var connection = factory.CreateConnection();
            return connection.CreateModel();*/
        }
    }
}
