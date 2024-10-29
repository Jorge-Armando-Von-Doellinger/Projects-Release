using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HMS.Payments.Messaging.Factory
{
    public sealed class ChannelFactory
    {
        private readonly MessagingSettings _settings;
        internal IModel Channel { get; }
        public ChannelFactory(IOptionsMonitor<MessagingSettings> settings)
        {
            _settings = settings.CurrentValue;
            Channel = GetChannel();
        }
        internal IModel GetChannel()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _settings.Hostname,
                UserName = _settings.User,
                Password = _settings.Password,
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            return channel;
        }
    }
}
