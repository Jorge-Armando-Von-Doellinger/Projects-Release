using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Models.Input;
using System.Text.Json;

namespace HMS.Payments.Application.Handlers.Message;

public sealed class PaymentEmployeeAddHandler : IMessageHandler
{
    private readonly IPaymentEmployeeManager _manager;
    private static JsonSerializerOptions _jsonOptions = new()
        { PropertyNamingPolicy = null, PropertyNameCaseInsensitive = true };
    public PaymentEmployeeAddHandler(IPaymentEmployeeManager manager)
    {
        _manager = manager;
    }

    public async Task HandleAsync(string json)
    {
        var payment = JsonSerializer.Deserialize<PaymentEmployeeModel>(json, _jsonOptions);
        await _manager.AddAsync(payment);
    }
}