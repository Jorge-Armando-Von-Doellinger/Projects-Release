using HMS.Payments.API.Services.Data;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Processor;
using System.Threading.Channels;
using System.Threading.Tasks.Dataflow;

namespace HMS.Payments.API.Services.Background
{
    public sealed class MessageConsumerService : BackgroundService
    {
        private readonly IMessageListener _messageListener;
        private readonly IServiceProvider _provider;
        private readonly IMessageProcessor _processorService;

        public MessageConsumerService(IMessageListener listener, IServiceProvider provider)
        {
            _messageListener = listener;
            this._provider = provider;
        }
        private readonly Channel<byte[]> _channel;
        private async Task StartListiner()
        {
            await _messageListener.ListeningAsync(async (bytes) =>
            {
                await _provider.CreateScope().ServiceProvider.GetRequiredService<IMessageProcessor>().Process(bytes);
            });
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartListiner();
        }
    }
}
