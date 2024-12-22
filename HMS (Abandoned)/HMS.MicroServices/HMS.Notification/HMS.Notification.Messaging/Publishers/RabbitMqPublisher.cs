using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using HMS.Notification.Core.Interfaces.Messaging;
using HMS.Notification.Messaging.Factory;
using RabbitMQ.Client;

namespace HMS.Notification.Messaging.Publishers;

public sealed class RabbitMqPublisher : IMessagePublisher
{
    private readonly ChannelFactory _channelFactory;

    public RabbitMqPublisher(ChannelFactory channelFactory)
    {
        _channelFactory = channelFactory;
    }

    public async Task PublishAsync(object message, string exchange, string queue, string routingKey)
    {
        var channel = await _channelFactory.GetChannelAsync();
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        await channel.BasicPublishAsync(exchange, queue, false, body);
    }

    public Task PublishToRetryAsync(object message)
    {
        throw new NotImplementedException();
    }
}