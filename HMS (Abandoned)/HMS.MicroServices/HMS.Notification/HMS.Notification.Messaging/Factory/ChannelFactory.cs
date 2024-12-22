using HMS.Notification.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HMS.Notification.Messaging.Factory;

public sealed class ChannelFactory
{
    private readonly MessagingSettings _settings; 

    public ChannelFactory(IOptionsMonitor<MessagingSettings> messagingSettings)
    {
        _settings = messagingSettings.CurrentValue;
    }

    internal async Task<IChannel> GetChannelAsync()
    {
        var channel = await GenerateChannelAsync();
        await ConfigureQueues(channel);
        return channel;
    }
    private async Task<IChannel> GenerateChannelAsync()
    {
        var uri = new Uri(_settings.Address);
        var factory = new ConnectionFactory()
        {
            HostName = uri.Host,
            Port = uri.Port,
            UserName = _settings.User,
            Password = _settings.Password
        };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        return channel;
    }

    private async Task ConfigureQueues(IChannel channel)
    {
        foreach (var queue in _settings.Queues.Values) // Usando a queue como routingKey
        {
            await channel.ExchangeDeclareAsync(_settings.Exchange, _settings.TypeExchange, true);
            await channel.ExchangeBindAsync(_settings.Exchange, _settings.Exchange, queue);
            await channel.QueueDeclareAsync(queue, true, false, false);
            await channel.QueueBindAsync(queue, _settings.Exchange, queue);
        }
    }
}