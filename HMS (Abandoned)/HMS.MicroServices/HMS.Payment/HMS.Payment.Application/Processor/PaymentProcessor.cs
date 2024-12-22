using HMS.Payments.Application.Interfaces.UseCases;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Parsers;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Processor;

namespace HMS.Payments.Application.Processor;

public sealed class PaymentProcessor : IMessageProcessor
{
    private readonly MessageParser _parser;
    private readonly IMessagePublisher _publisher;
    private readonly IPaymentUseCase _useCase;

    public PaymentProcessor(IPaymentUseCase useCase,
        IMessagePublisher publisher,
        MessageParser parser)
    {
        _useCase = useCase;
        _publisher = publisher;
        _parser = parser;
    }

    public async Task ProcessMessageAsync(byte[] message)
    {
        try
        {
            var payment = await _parser.Parse<PaymentModel>(message);
            await _useCase.AddAsync(payment);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _publisher.ToRetryQueueAsync(new Message { Content = message, Attempts = 1 });
        }
    }

    public async Task ProcessBatchMessageAsync(IEnumerable<byte[]> batch)
    {
        var payments = new List<PaymentModel>();
        await Parallel.ForEachAsync(batch, async (message, c) =>
        {
            try
            {
                var payment = await _parser.Parse<PaymentModel>(message);
                payments.Add(payment);
            }
            catch (Exception ex)
            {
                await _publisher.ToRetryQueueAsync(new Message { Content = message, Attempts = 1 }); // Concertar isso
            }
        });
        await _useCase.AddBatchAsync(payments);
    }

    public async Task ReProcessMessageAsync(Message message)
    {
        throw new NotImplementedException();
    }
}