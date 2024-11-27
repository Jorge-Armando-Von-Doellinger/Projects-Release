using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Models.Update;
using System.Text.Json;

namespace HMS.Payments.Application.Handlers.Message;

public sealed class PaymentEmplyoeeUpdateHandler : IMessageHandler
{
    private readonly IPaymentEmployeeManager _manager;

    public PaymentEmplyoeeUpdateHandler(IPaymentEmployeeManager manager)
    {
        _manager = manager;
    }

    public async Task HandleAsync(string json)
    {
        var payment = JsonSerializer.Deserialize<PaymentEmployeeUpdateModel>(json);
        await _manager.UpdateAsync(payment);
    }
}