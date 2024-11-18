using HMS.Payments.Core.Data;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Processor;
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
        private IModel _channel;
        private readonly IMessageProcessor _messageProcessor;
        private readonly IMessagePublisher _messagePublisher;
        private readonly MessagingSettings _settings;

        public MessageListener(IServiceProvider serviceProvider, IOptionsMonitor<MessagingSettings> settings , IMessageProcessor messageProcessor)
        {
            _channel = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IModel>();
            _messageProcessor = messageProcessor;
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
            
            
            cancellationToken.ThrowIfCancellationRequested();
            _channel.BasicQos(0, 50, false);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (obj, args) =>
            {
                var message = args.Body.ToArray();
                lock (_messages)
                    _messages.Add(message);
            };
            
            Parallel.ForEach(_settings.Queues, (queue, cancellationToken) =>
            {
                _channel.BasicConsume(queue, true, consumer);
            });
        }

        private async Task AddOnRetryQueueAsync(List<MessageData> messages)
        {
            foreach (var message in messages)
            {
                await _messagePublisher.ReRepublishAsync(message, 
                    _settings.Exchange, 
                    _settings.Queues.FirstOrDefault(x => x.Contains("retry")), 
                    string.Empty);
            }
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ListeningAsync(stoppingToken);
            List<byte[]> copy;
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                lock (_messages) copy = new List<byte[]>(_messages); // Create a copy of the messages
                var messagesWithError = await _messageProcessor.TryProcess(copy);
                if (!messagesWithError.success && messagesWithError.MessageWithErrors?.Count > 0)
                    await AddOnRetryQueueAsync(messagesWithError.MessageWithErrors);
            }
        }
        
        // Confirma recebimento
        // Depois processa em lote
        // Os que derem erro, serão enviados para a queue de retry
        // Os que não são possiveis processar, vão para a dead-queue
    }
}
