using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Models.Update;
using System.Text.Json;

namespace HMS.Payments.Application.Handlers.Message
{
    public sealed class PaymentUpdateHandler : IMessageHandler
    {
        private readonly IPaymentManager _paymentManager;
        public PaymentUpdateHandler(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        public async Task HandleAsync(string json)
        {
            var desserialized = JsonSerializer.Deserialize<PaymentUpdateModel>(json);
            await _paymentManager.UpdateAsync(desserialized);
        }
    }
}
