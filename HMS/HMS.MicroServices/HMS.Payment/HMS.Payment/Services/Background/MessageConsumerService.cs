using HMS.Payments.API.Services.Data;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Processor;

namespace HMS.Payments.API.Services.Background
{
    public sealed class MessageConsumerService : BackgroundService
    {
        private readonly IMessageListener _messageListener;
        private readonly IMessageProcessor _processorService;

        public MessageConsumerService(IMessageListener listener, IMessageProcessor messageProcessorService)
        {
            _messageListener = listener;
            _processorService = messageProcessorService;
        }

        private async Task StartListiner()
        {
            
            await _messageListener.ListeningAsync(async (bytes) =>
            {
                try
                {
                    await _processorService.Process(bytes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao ao processar mensagem!");
                    Console.WriteLine("     Mensagem: ---> " + ex.Message);
                    throw;
                }
            });
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartListiner();
        }
    }
}
