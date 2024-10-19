
using HMS.ContractsMicroService.API.Services.Data;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;

namespace HMS.ContractsMicroService.API.Services.Background
{
    public class MessageListenerService : BackgroundService
    {
        private readonly Lazy<IMessageListener> _listener;
        private readonly Lazy<IMessageProcessor> _processor;

        public MessageListenerService(Lazy<IMessageListener> listener, Lazy<IMessageProcessor> processor)
        {
            _listener = listener;
            _processor = processor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await SettingsStartupState.AwaitSettingsCompletionAsync();
            await _listener.Value.StartListener(async (data) =>
            {
                await _processor.Value.Process(data);
            });
            stoppingToken.ThrowIfCancellationRequested();
        }
    }
}
