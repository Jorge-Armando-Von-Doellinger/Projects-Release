using System.Diagnostics;
using System.Text;
using HMS.Payments.Core.Data;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Processor;
using HMS.Payments.Messaging.Factory;
using HMS.Payments.Messaging.Properties;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HMS.Payments.Messaging.Listeners
{
    public sealed class MessageListener : BackgroundService, IMessageListener
    {
        private readonly IMessageProcessor _messageProcessor;
        private readonly ChannelFactory _factory;
        private readonly MessagingSettings _settings;

        public MessageListener(IServiceProvider serviceProvider, 
            IOptionsMonitor<MessagingSettings> settings, 
            IMessageProcessor messageProcessor,
            ChannelFactory factory)
        {
            _messageProcessor = messageProcessor;
            _factory = factory;
            _settings = settings.CurrentValue;
        }
        
        private List<byte[]> _messages = new ();

        public async Task ListeningAsync(CancellationToken cancellationToken)
        {
            var channel = await _factory.GetChannelAsync();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (obj, args) =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                var message = args.Body.ToArray();
                await _messageProcessor.BatchProcess(message);
                await channel.BasicAckAsync(args.DeliveryTag, false);
            };
            
            await Parallel.ForEachAsync(_settings.Queues.Values, cancellationToken, async (queue, cancellationToken) =>
            {
                await channel.BasicConsumeAsync(queue, false, consumer, cancellationToken);
            });
        }
        
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ListeningAsync(stoppingToken);
        }
    }
}
