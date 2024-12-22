using System.Text;
using System.Text.Json;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Messaging.Context;
using HMS.Payments.Messaging.Factory;
using HMS.Payments.Messaging.Properties;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HMS.Payments.Messaging.Publisher;

public sealed class MessagePublisher : IMessagePublisher
{
    private readonly ChannelFactory _channelFactory;
    private readonly MessagingSettings _settings;

    private readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNamingPolicy = null
    };

    public MessagePublisher(ChannelFactory channelFactory, RabbitContext context,
        IOptionsMonitor<MessagingSettings> messagingSettings)
    {
        _channelFactory = channelFactory;
        _settings = messagingSettings.CurrentValue;
    }

    public async Task PublishAsync<T>(T message, string exchange, string queue, string routingkey)
    {
        var channel = await _channelFactory.GetChannelAsync();
        var serialized = JsonSerializer.Serialize(message, jsonOptions);
        var bytes = Encoding.UTF8.GetBytes(serialized);
        await channel.BasicPublishAsync(exchange, routingkey, false, bytes);
    }

    public async Task ToRetryQueueAsync(Message message)
    {
        var channel = await _channelFactory.GetChannelAsync();
        var serialized = JsonSerializer.Serialize(message.Content, jsonOptions);
        var bytes = Encoding.UTF8.GetBytes(serialized);
        var properties = new BasicProperties().Headers = new Dictionary<string, object?>
        {
            { new MessageProperties().RetryCount, message.Attempts }
        };
        await channel.BasicPublishAsync(_settings.Exchange, _settings.GetRetryQueue(), false, bytes);
    }

    public async Task ToDeadLetterQueueAsync<T>(T message)
    {
        var channel = await _channelFactory.GetChannelAsync();
        var serialized = JsonSerializer.Serialize(message, jsonOptions);
        var bytes = Encoding.UTF8.GetBytes(serialized);
        await channel.BasicPublishAsync(_settings.Exchange, _settings.GetUnprocessableQueue(), false, bytes);
    }
}