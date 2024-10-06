
using HMS.ContractsMicroService.Core.Interfaces.Messaging;

namespace HMS.ContractsMicroService.API.Services.Background
{
    public class MessageListenerService : BackgroundService
    {
        private readonly IMessageListener _listener;
        public MessageListenerService(IMessageListener listener)
        {
            _listener = listener;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            await _listener.StartListener();
        }
    }
}
