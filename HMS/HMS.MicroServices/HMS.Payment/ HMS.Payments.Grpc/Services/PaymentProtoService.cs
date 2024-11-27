using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Manager;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;

namespace HMS.Payments.Grpc.Services;

public sealed class PaymentProtoService : PaymentProto.PaymentProtoBase
{
    private readonly IPaymentManager _manager;
    private readonly IMessagePublisher _publisher;
    private readonly IOptionsMonitor<MessagingSettings> _messagingSettings;

    public PaymentProtoService(IPaymentManager manager, 
        IMessagePublisher publisher,
        IOptionsMonitor<MessagingSettings> messagingSettings)
    {
        _manager = manager;
        _publisher = publisher;
        _messagingSettings = messagingSettings;
    }
    public override async Task<Empty> AddPayment(PaymentProtoDto request, ServerCallContext context)
    {
        var model = new PaymentModel()
        {
            Amount = request.Amount,
            Description = request.Description,
            PaymentMethod = request.PaymentMethod,
            Beneficiary = request.Beneficiary,
            Observations = request.Observations,
            Payer = request.Payer,
            BeneficiaryCPF = request.BeneficiaryCPF,
            PayerCPF = request.PayerCPF,
            BeneficiaryCNPJ = request.BeneficiaryCNPJ,
            PayerCNPJ = request.PayerCNPJ,
        };
        var settings = _messagingSettings.CurrentValue;
        await Parallel.ForAsync(1, 100, async (value, cancellationToken) =>
        {
            await _publisher.PublishAsync(model, settings.Exchange ,settings.GetPaymentQueue(), settings.GetPaymentQueue());
        });
        await _manager.AddAsync(model);
        return new Empty();
    }
}