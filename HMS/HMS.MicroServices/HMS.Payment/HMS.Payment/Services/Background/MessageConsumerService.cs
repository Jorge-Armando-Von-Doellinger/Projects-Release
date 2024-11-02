using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Services;

namespace HMS.Payments.API.Services.Background
{
    public sealed class MessageConsumerService : BackgroundService
    {
        private readonly IMessageListener _messageListener;
        private readonly IMessageProcessorService _processorService;

        public MessageConsumerService(IMessageListener listener, IMessageProcessorService messageProcessorService)
        {
            _messageListener = listener;
            _processorService = messageProcessorService;
        }

        private async Task StartListiner()
        {
            await _messageListener.ListeningAsync(async (bytes) =>
            {
                await Task.Delay(1000);
                await _processorService.Process(bytes);
            });
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartListiner();
        }
    }
}
