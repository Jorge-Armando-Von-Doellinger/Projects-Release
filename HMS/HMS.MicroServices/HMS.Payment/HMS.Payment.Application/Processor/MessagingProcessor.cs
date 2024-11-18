using HMS.Payments.Application.Enums.Keys.Schemas;
using HMS.Payments.Application.Exceptions;
using HMS.Payments.Application.Handlers.Message;
using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Interfaces.Services;
using HMS.Payments.Core.Interfaces.Processor;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using System.Text;
using HMS.Payments.Application.Services;
using HMS.Payments.Core.Data;

namespace HMS.Payments.Application.Processor
{
    public sealed class MessagingProcessor : IMessageProcessor
    {
        private readonly ISchemasModelService _schemasModelService;
        private readonly ICacheService _cacheService;
        private readonly IPaymentManager _paymentManager;
        private readonly IPaymentEmployeeManager _paymentEmployeeManager;
        private readonly HandlerService _handlerService;

        public MessagingProcessor(IServiceProvider serviceProvider, ICacheService cacheService)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            _paymentEmployeeManager = scope.GetRequiredService<IPaymentEmployeeManager>();
            _paymentManager = scope.GetRequiredService<IPaymentManager>();
            _handlerService = scope.GetRequiredService<HandlerService>();
            _cacheService = cacheService;
            
        }

        
        public async Task TryProcess(byte[] bytes)
        {
            var json = Encoding.UTF8.GetString(bytes);
            var handle = _handlerService.GetHandler(json)
                            ?? throw new InvalidMessageException("Mensagem inválida!");
            await handle.HandleAsync(json);
        }

        public async Task<(bool success, List<MessageData>? MessageWithErrors)> TryProcess(List<byte[]> bytes)
        {
            var messages = new List<MessageData>();
            Parallel.ForEach(bytes, async (bytes, _) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(bytes);
                    var handle = _handlerService.GetHandler(json) 
                                ?? throw new InvalidMessageException("Mensagem inválida!");
                    await handle.HandleAsync(json);
                }
                catch
                {
                    messages.Add(new() { Message = bytes, RetryCount = 1});
                }
            });
            return new (){ success = true, MessageWithErrors = messages };
        }
    }
}
