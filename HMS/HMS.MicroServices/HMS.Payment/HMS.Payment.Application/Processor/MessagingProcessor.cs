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
using System.Text.Json;
using HMS.Payments.Application.Services;
using HMS.Payments.Core.Data;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Messaging;

namespace HMS.Payments.Application.Processor
{
    public sealed class MessagingProcessor : IMessageProcessor
    {
        private readonly ISchemasModelService _schemasModelService;
        private readonly ICacheService _cacheService;
        private readonly IPaymentManager _paymentManager;
        private readonly IPaymentEmployeeManager _paymentEmployeeManager;
        private readonly HandlerService _handlerService;
        private readonly IMessagePublisher _publisher;

        public MessagingProcessor(IServiceProvider serviceProvider, ICacheService cacheService)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            _paymentEmployeeManager = scope.GetRequiredService<IPaymentEmployeeManager>();
            _paymentManager = scope.GetRequiredService<IPaymentManager>();
            _handlerService = scope.GetRequiredService<HandlerService>();
            _cacheService = cacheService;
            _publisher = serviceProvider.GetRequiredService<IMessagePublisher>();
        }

        
        public async Task Process(byte[] bytes)
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
                await _publisher.ToRetryQueue(new() { Content = bytes, Attempts = 1 });
            }
        }

        public async Task Process(List<byte[]> bytes)
        {
            var messages = new List<Message>();
            await Parallel.ForEachAsync(bytes, async (bytes, _) =>
            {
                short attempts = 0;
                try
                {
                    var json = Encoding.UTF8.GetString(bytes);
                    var handle = _handlerService.GetHandler(json) 
                                ?? throw new InvalidMessageException("Mensagem inválida!");
                    await handle.HandleAsync(json);
                }
                catch
                {
                    messages.Add(new() { Content = bytes, Attempts = 1});
                }
            });
            if(messages.Count < 1) return; // Se não houver mensagens não processadas, ele retorna,
            await Parallel.ForEachAsync(messages, async (message, cancelationToken) =>
            {
                await _publisher.ToRetryQueue(message);
            });
        }

        public async Task ReProcess(Message message) // Tenta reprocessar uma mensagem. Se não conseguir, joga na fila dnv
        {
            try
            {
                var json = Encoding.UTF8.GetString(message.Content);
                var handle = _handlerService.GetHandler(json)
                             ?? throw new InvalidMessageException("Mensagem inválida!");
                await handle.HandleAsync(json);
            }
            catch
            {
                message.Attempts++;
                await _publisher.ToRetryQueue(message);
            }
        }
    }
}
