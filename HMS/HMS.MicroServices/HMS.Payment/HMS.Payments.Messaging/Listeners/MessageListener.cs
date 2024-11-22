using System.Diagnostics;
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
        private readonly IMessagePublisher _messagePublisher;
        private readonly MessagingSettings _settings;

        public MessageListener(IServiceProvider serviceProvider, 
            IOptionsMonitor<MessagingSettings> settings , 
            IMessageProcessor messageProcessor,
            ChannelFactory factory)
        {
            _messageProcessor = messageProcessor;
            _factory = factory;
            _messagePublisher  = serviceProvider.GetRequiredService<IMessagePublisher>();
            _settings = settings.CurrentValue;
        }
        
        private List<byte[]> _messages = new ();

        public async Task ListeningAsync(CancellationToken cancellationToken)
        {
            // Fazer uma forma de poder pegar as mensagens sem lock
            // Fazer aqui o processamento em lote
            // Quando der um erro, uso um try-catch para RECEBER a lista dos que deram errodo e jogo na fila de requeue
            // Processar tudo
            // retornar para a fila de retry os que derem erro
            // quem der erro +3x, recebe a notificação de erro
            // vai para dead-queue
            var channel = await _factory.GetChannelAsync();
            
            
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (obj, args) =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                var message = args.Body.ToArray();
                var retries = GetRetries(args.BasicProperties.Headers);
                Task task = retries switch 
                {
                    0 => _messageProcessor.Process(message),
                    <=3 => _messageProcessor.ReProcess(new () {Content = message, Attempts = retries}),
                    >3 => _messagePublisher.ToDeadLetterQueueAsync(new Message() { Content = message, Attempts = retries })
                };
                await task;
                // if(retries == 0)
                //     await _messageProcessor.Process(message);
                // else if(retries <= 3)
                //     await _messageProcessor.ReProcess(new () { Content = message, });
                // else
                //     await _messagePublisher.ToDeadLetterQueueAsync(new Message() { Content = message, Attempts = 4});
            };
            
            await Parallel.ForEachAsync(_settings.Queues.Values, async (queue, cancellationToken) =>
            {
                await channel.BasicConsumeAsync(queue, false, consumer);
            });
        }

        private short GetRetries(IDictionary<string, object> headers)
        {
            if(headers.TryGetValue(new MessageProperties().RetryCount, out var retryCount))
                return (short) retryCount;
            return 0;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ListeningAsync(stoppingToken);
        }
        
        // Confirma recebimento
        // Depois processa em lote
        // Os que derem erro, serão enviados para a queue de retry
        // Os que não são possiveis processar, vão para a dead-queue
    }
}
