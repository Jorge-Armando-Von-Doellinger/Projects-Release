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
        public MessageConsumerService(IMessageListener listener/*, IServiceProvider provider, Channel<byte[]> channel*/)
        {
            _messageListener = listener;
            /*this._provider = provider;
            _channel = channel;*/
        }
/*        private readonly Channel<byte[]> _channel;
        private async Task StartListiner()
        {
            Console.WriteLine("a");
            while (await _channel.Reader.WaitToReadAsync())
            {
                if (_channel.Reader.TryRead(out var message))
                {
                    // Aguarda um slot disponível no semáforo
                    await _semaphore.WaitAsync();

                    _ = Task.Run(async () =>
                    {
                        Console.WriteLine("asdw");
                        try
                        {
                            // Cria um escopo de serviço para resolver IMessageProcessor
                            using var scope = _provider.CreateScope();
                            var processor = scope.ServiceProvider.GetService<IMessageProcessor>();

                            if (processor != null)
                            {
                                await processor.Process(message); // Processa a mensagem
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao processar mensagem: " + ex.Message);
                        }
                        finally
                        {
                            // Libera o slot no semáforo
                            _semaphore.Release();
                        }
                    });
                }
            }
        }*/
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageListener.ListeningAsync();
        }
    }
}
