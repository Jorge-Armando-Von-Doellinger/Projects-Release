using System.Text.Json;
using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.UseCases;
using HMS.Payments.Application.Models.Input;

namespace HMS.Payments.Application.Handlers.Message;

public sealed class PaymentEmployeeAddHandler : IMessageHandler
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
        { PropertyNamingPolicy = null, PropertyNameCaseInsensitive = true };

    private readonly IPaymentEmployeeUseCase _useCase;

    public PaymentEmployeeAddHandler(IPaymentEmployeeUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task HandleAsync(string json)
    {
        var payment = JsonSerializer.Deserialize<PaymentEmployeeModel>(json, _jsonOptions);
        await _useCase.AddAsync(payment);
    }
}