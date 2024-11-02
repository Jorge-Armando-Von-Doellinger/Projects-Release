using HMS.Payments.Application.Enums.Keys.Schemas;
using HMS.Payments.Application.Exceptions;
using HMS.Payments.Application.Handlers.Message;
using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Interfaces.Services;
using HMS.Payments.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using System.Text;

namespace HMS.Payments.Application.Services
{
    public sealed class MessagingProcessorService : IMessageProcessorService
    {
        private Dictionary<JsonSchema, IMessageHandler> _handler = new();
        private readonly IPaymentEmployeeManager _paymentEmployeeManager;
        private readonly IPaymentManager _paymentManager;
        private readonly ICacheService _cacheService;

        public MessagingProcessorService(IServiceProvider serviceProvider, ICacheService cacheService)
        {
            var scope = serviceProvider.CreateScope();
            _paymentManager = scope.ServiceProvider.GetRequiredService<IPaymentManager>();
            var scope2= serviceProvider.CreateScope();
            _paymentEmployeeManager = scope2.ServiceProvider.GetRequiredService<IPaymentEmployeeManager>();
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
            var a = GetSchema(SchemasDefaultKeysEnum.Payment_Add_Schema);
            if (a == null) throw new Exception("Batata");
            _handler.Add(GetSchema(SchemasDefaultKeysEnum.Payment_Add_Schema), new PaymentAddHandler(_paymentManager));
            _handler.Add(GetSchema(SchemasDefaultKeysEnum.Payment_Update_Schema), new PaymentUpdateHandler(_paymentManager));
        }

        private void AddPaymentEmployeeHandler()
        {

        }
        private JsonSchema GetSchema(SchemasDefaultKeysEnum key)
        {
            
            return _cacheService.Get<JsonSchema>(key.ToString());
            
        }
        public async Task Process(byte[] bytes)
        {
            var json = Encoding.UTF8.GetString(bytes);

            var handle = _handler
                .FirstOrDefault(x => x.Key.Validate(json).Count == 0).Value
                ?? throw new MessageInvalidException("Esta mensagem não é valida para nenhum pagamento!");
            await handle.HandleAsync(json);
        }
    }
}
