using Gateway.v1.Messaging.Configurator;
using Nuget.MessagingUtilities.MessageSettings;
using RabbitMQ.Client;

namespace Gateway.v1.Messaging.Factory
{
    public sealed class ChannelFactory
    {
        public IModel Channel { get; private set; }
        public ChannelFactory()
        {
            GenerateChannel();
        }

        private async Task GenerateChannel()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost" //Container ou local!
                };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                Channel = channel;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
