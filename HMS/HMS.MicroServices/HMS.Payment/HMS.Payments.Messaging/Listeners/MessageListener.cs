using System.Collections.Concurrent;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Processor;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HMS.Payments.Messaging.Listeners
{
    public sealed class MessageListener : IMessageListener
    {
        private IModel _channel;
        private readonly IMessageProcessor _messageProcessor;
        private List<byte[]> _messages = new ();
        private List<ulong> _tags = new ();
        private readonly MessagingSettings _settings;

        public MessageListener(IServiceProvider serviceProvider, IOptionsMonitor<MessagingSettings> settings , IMessageProcessor messageProcessor)
        {
            _channel = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IModel>();
            _messageProcessor = messageProcessor;
            _settings = settings.CurrentValue;
        }

        public ulong[] Tags => _tags.ToArray();

        public List<byte[]> MessageBytes => _messages.ToList();
        

        public async Task ListeningAsync(CancellationToken cancellationToken)
        {
            // Fazer uma forma de poder pegar as mensagens sem lock
            // Fazer aqui o processamento em lote
            // Quando der um erro, uso um try-catch para RECEBER a lista dos que deram errodo e jogo na fila de requeue
            
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Run(() =>
            {
                _channel.BasicQos(0, 50, false);
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += async (obj, args) =>
                {
                    
                    _messages.Add(args.Body.ToArray());
                    _tags.Add(args.DeliveryTag);
                };
                _settings.Queues.ForEach(queue =>
                {
                    _channel.BasicConsume(queue, true, consumer);
                });
            });
        }
        
    }
}
