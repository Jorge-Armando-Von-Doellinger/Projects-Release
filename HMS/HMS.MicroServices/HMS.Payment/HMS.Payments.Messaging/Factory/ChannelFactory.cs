using HMS.Payments.Messaging.Context;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HMS.Payments.Messaging.Factory
{
    public sealed class ChannelFactory
    {
        private readonly RabbitContext _context;
        private readonly MessagingSettings _settings;
        public ChannelFactory(IOptionsMonitor<MessagingSettings> settings, RabbitContext context)
        {
            _context = context;
            _settings = settings.CurrentValue;
        }
        internal async Task<IChannel> GetChannelAsync()
        {
            var uri = new Uri(_settings.Address);
            var factory = new ConnectionFactory()
            {
                HostName = uri.Host,
                Port = uri.Port,
                UserName = _settings.User,
                Password = _settings.Password,
            };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            await _context.ConfigureChannel(channel);
            return channel;
        }
    }
}
