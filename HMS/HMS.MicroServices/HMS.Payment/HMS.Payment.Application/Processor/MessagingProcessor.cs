﻿using HMS.Payments.Application.Enums.Keys.Schemas;
using HMS.Payments.Application.Exceptions;
using HMS.Payments.Application.Handlers.Message;
using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Interfaces.Services;
using HMS.Payments.Core.Interfaces.Processor;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using System.Text;

namespace HMS.Payments.Application.Processor
{
    public sealed class MessagingProcessor : IMessageProcessor
    {
        private Dictionary<JsonSchema, IMessageHandler> _handler = new();
        private readonly ISchemasModelService _schemasModelService;
        private readonly ICacheService _cacheService;
        private readonly IPaymentManager _paymentManager;
        private readonly IPaymentEmployeeManager _paymentEmployeeManager;

        public MessagingProcessor(IServiceProvider serviceProvider, ISchemasModelService schemasModelService, ICacheService cacheService)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            _paymentEmployeeManager = scope.GetRequiredService<IPaymentEmployeeManager>();
            _paymentManager = scope.GetRequiredService<IPaymentManager>();
            _schemasModelService = schemasModelService;
            _cacheService = cacheService;
            AddHandlers();
        }

        private void AddHandlers()
        {
            AddPaymentHandler();
            AddPaymentEmployeeHandler();
        }
        private void AddPaymentHandler()
        {
            _handler.Add(GetSchema(SchemasDefaultKeysEnum.Payment_Add_Schema), new PaymentAddHandler(_paymentManager));
            _handler.Add(GetSchema(SchemasDefaultKeysEnum.Payment_Update_Schema), new PaymentUpdateHandler(_paymentManager));
        }
        private void AddPaymentEmployeeHandler()
        {
            _handler.Add(GetSchema(SchemasDefaultKeysEnum.PaymentEmployee_Add_Schema), new PaymentEmployeeAddHandler(_paymentEmployeeManager));
            _handler.Add(GetSchema(SchemasDefaultKeysEnum.PaymentEmployee_Update_Schema), new PaymentEmplyoeeUpdateHandler(_paymentEmployeeManager));
        }
        private JsonSchema GetSchema(SchemasDefaultKeysEnum key)
        {
            var data = _schemasModelService.GetDtosSchemas().FirstOrDefault(x => x.Key == key).Value;
            return data;
        }
        public async Task Process(byte[] bytes)
        {
            try
            {
                var json = Encoding.UTF8.GetString(bytes);
                var handle = _handler
                    .FirstOrDefault((x) => x.Key.Validate(json, new()).Count == 0)
                    .Value ?? throw new InvalidMessageException("Mensagem inválida!");
                await handle.HandleAsync(json);
            }
            catch
            {
                throw;
            }
        }

        public async Task Process(List<byte[]> bytes)
        {
            if (bytes.Count < 100) return;
            await Parallel.ForEachAsync(bytes, async (data, _) =>
            {
                var json = Encoding.UTF8.GetString(data);
                var handle = _handler
                    .FirstOrDefault((x) => x.Key.Validate(json, new()).Count == 0)
                    .Value ?? throw new InvalidMessageException("Mensagem inválida!");
                
                await handle.HandleAsync(json);
            });
        }
    }
}
