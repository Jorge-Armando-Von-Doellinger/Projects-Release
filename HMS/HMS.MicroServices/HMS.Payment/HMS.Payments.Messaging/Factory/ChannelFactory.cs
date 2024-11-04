using HMS.Payments.Messaging.Context;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;

namespace HMS.Payments.Messaging.Factory
{
    public sealed class ChannelFactory
    {
        private readonly MessagingSettings _settings;
        internal IModel Channel { get; }
        public ChannelFactory(IOptionsMonitor<MessagingSettings> settings, RabbitContext context)
        {
            _settings = settings.CurrentValue;
            Channel = GetChannel();
            context.ConfigureChannel(Channel);
        }
        internal IModel GetChannel()
        {
            var uri = new Uri(_settings.Address);
            var factory = new ConnectionFactory()
            {
                HostName = uri.Host,
                Port = uri.Port,
                UserName = _settings.User,
                Password = _settings.Password,
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            return channel;
        }
    }
}
