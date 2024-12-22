using HMS.Payments.Application.Models.Input;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HMS.Payments.Console;

public class TestClass
{
    private readonly IServiceProvider _serviceProvider;

    public TestClass(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    internal async Task RunAsync()
    {
        var scope = _serviceProvider.CreateAsyncScope();
        var pub = scope.ServiceProvider.GetRequiredService<IMessagePublisher>();
        var settings = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<MessagingSettings>>().CurrentValue;
        var model = new PaymentModel
        {
            Amount = 12.45,
            Beneficiary = "Jonas",
            Description = "Jonas",
            Observations = "Pedrodwasdwasdwas",
            Payer = "Jonas",
            PaymentMethod = "Pix",
            BeneficiaryCPF = "809.145.210-12",
            PayerCPF = "809.145.210-12"
        };

        var queue = settings.GetPaymentQueue();
        await Parallel.ForAsync(1, 500,
            async (i, c) =>
            {
                await pub.PublishAsync(model, settings.Exchange, queue, settings.GetPaymentQueue());
                await pub.PublishAsync(model, settings.Exchange, queue, settings.GetPaymentQueue());
            });
    }
}