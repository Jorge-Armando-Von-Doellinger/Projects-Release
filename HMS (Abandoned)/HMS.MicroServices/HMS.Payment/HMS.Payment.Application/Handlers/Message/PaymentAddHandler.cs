using System.Text.Json;
using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.UseCases;
using HMS.Payments.Application.Models.Input;

namespace HMS.Payments.Application.Handlers.Message;

public sealed class PaymentAddHandler : IMessageHandler
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
        { PropertyNamingPolicy = null, PropertyNameCaseInsensitive = true };

    private readonly IPaymentUseCase _iPaymentUseCase;

    public PaymentAddHandler(IPaymentUseCase iPaymentUseCase)
    {
        _iPaymentUseCase = iPaymentUseCase;
    }

    public async Task HandleAsync(string json)
    {
        var desserialized = JsonSerializer.Deserialize<PaymentModel>(json, _jsonOptions);
        await _iPaymentUseCase.AddAsync(desserialized);
    }
}