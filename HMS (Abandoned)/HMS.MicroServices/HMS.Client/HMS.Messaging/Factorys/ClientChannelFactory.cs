using HMS.Messaging.Configurators;
using RabbitMQ.Client;

namespace HMS.Messaging.Factorys
{
    public class ClientChannelFactory
    {
        public IModel Channel { get; private set; }
        private readonly ClientChannelConfigurator _configurator;
        public ClientChannelFactory(ClientChannelConfigurator configurator)
        {
            _configurator = configurator;
            ChannelGenerate();
        }
        private void ChannelGenerate()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            _configurator.SetConfigs(channel);
            Channel = channel;
        }
    }
}
