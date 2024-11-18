using HMS.Payments.Core.Interfaces.Messaging;

namespace HMS.Payments.API.Services.Background
{
    public sealed class MessageConsumerService : BackgroundService
    {
        private readonly IMessageListener _messageListener;
        /*        private readonly IServiceProvider _provider;
                private readonly IMessageProcessor _processorService;
                private readonly SemaphoreSlim _semaphore = new(50);
        */
        public MessageConsumerService(IMessageListener listener)
        {
            _messageListener = listener;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageListener.ListeningAsync(stoppingToken);
        }
    }
}
