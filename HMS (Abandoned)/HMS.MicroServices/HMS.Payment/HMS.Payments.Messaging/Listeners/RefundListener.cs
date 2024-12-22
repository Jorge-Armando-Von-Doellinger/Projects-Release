using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Processor;
using HMS.Payments.Messaging.Configs;
using HMS.Payments.Messaging.Factory;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HMS.Payments.Messaging.Listeners;

public sealed class RefundListener : IMessageListener
{
    private readonly ChannelFactory _channelFactory;
    private readonly IMessageProcessor _processor;
    private readonly MessagingSettings _settings;

    public RefundListener(IServiceProvider provider,
        ChannelFactory channelFactory,
        IOptionsMonitor<MessagingSettings> settings)
    {
        _processor = provider
            .CreateAsyncScope()
            .ServiceProvider
            .GetRequiredKeyedService<IMessageProcessor>(MessageProcessorSelectorEnum.Refund.ToString());
        _channelFactory = channelFactory;
        _settings = settings.CurrentValue;
    }

    public async Task ListeningAsync(CancellationToken cancellationToken)
    {
        var channel = await _channelFactory.GetChannelAsync();
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (_, args) =>
        {
            var body = args.Body.ToArray();
            await _processor.ProcessMessageAsync(body);
            await channel.BasicAckAsync(args.DeliveryTag, false, cancellationToken);
        };
        await channel.BasicConsumeAsync(_settings.GetRefundQueue(), false, consumer, cancellationToken);
    }
}