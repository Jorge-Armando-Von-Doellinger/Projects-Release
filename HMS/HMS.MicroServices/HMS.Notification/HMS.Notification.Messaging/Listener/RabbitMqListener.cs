using System.Collections.Concurrent;
using System.Text;
using HMS.Notification.Core.Interfaces.Messaging;
using HMS.Notification.Messaging.Factory;
using HMS.Notification.Messaging.MessageHeaders;
using HMS.Notification.Messaging.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HMS.Notification.Messaging.Listener;

public sealed class RabbitMqListener : BackgroundService, IMessageListener
{
    private readonly ChannelFactory _channelFactory;
    private readonly IMessageProcessor _processor;
    private readonly MessagingSettings _settings;
    private const int RetryLimit = 3;

    public RabbitMqListener(ChannelFactory channelFactory, 
        IMessageProcessor processor, 
        IOptionsMonitor<MessagingSettings> settings)
    {
        _channelFactory = channelFactory;
        _processor = processor;
        _settings = settings.CurrentValue;
    }
    public async Task StartListeningAsync(CancellationToken cancellationToken)
    {
        var channel = await _channelFactory.GetChannelAsync();
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            if(await UnprocessableByRetryCount(ea))
                return;
            await _processor.Process(ea.Body.ToArray());
            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };
        var tasks = new List<Task>();
        foreach (var queue in _settings.Queues.Values)
        {
            tasks.Add(channel.BasicConsumeAsync(queue, false, consumer));
        }
        await Task.WhenAll(tasks);
    }

    private async Task<bool> UnprocessableByRetryCount(BasicDeliverEventArgs ea)
    {
        if (ea.BasicProperties.Headers == default) return false;
        var headerValue = ea.BasicProperties.Headers.FirstOrDefault(x => x.Key == DefaultMessageHeader.RetryCount).Value;
        if(headerValue == null) return false;
        if(headerValue is int retryCount)
            return retryCount >= RetryLimit;
        return true;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Starting RabbitMq listener");
        await StartListeningAsync(stoppingToken);
    }
}