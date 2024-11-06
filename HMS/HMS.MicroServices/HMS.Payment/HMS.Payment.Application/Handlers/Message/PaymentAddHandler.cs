using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Models.Input;
using System.Text.Json;

namespace HMS.Payments.Application.Handlers.Message
{
    public sealed class PaymentAddHandler : IMessageHandler
    {
        private readonly IPaymentManager _paymentManager;

        public PaymentAddHandler(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        public async Task HandleAsync(string json)
        {
            var desserialized = JsonSerializer.Deserialize<PaymentModel>(json);
            await _paymentManager.AddAsync(desserialized);
        }
    }
}
