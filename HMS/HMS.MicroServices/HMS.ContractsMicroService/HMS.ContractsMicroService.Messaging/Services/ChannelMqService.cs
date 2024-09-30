using RabbitMQ.Client;

namespace HMS.ContractsMicroService.Messaging.Messaging
{
    public sealed class ChannelMqService
    {
        private IModel _channel;

        public ChannelMqService()
        {
            SetChannel();
        }
        private void SetChannel()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }]
        internal void ConfigureResponseChannel()
        {

        }
    }
}
