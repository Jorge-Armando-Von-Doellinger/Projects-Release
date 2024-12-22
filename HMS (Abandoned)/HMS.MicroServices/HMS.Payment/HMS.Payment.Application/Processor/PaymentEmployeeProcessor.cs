using HMS.Payments.Application.Interfaces.UseCases;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Parsers;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Processor;

namespace HMS.Payments.Application.Processor;

public sealed class PaymentEmployeeProcessor : IMessageProcessor
{
    private readonly MessageParser _parser;
    private readonly IMessagePublisher _publisher;
    private readonly IPaymentEmployeeUseCase _useCase;

    public PaymentEmployeeProcessor(IMessagePublisher publisher,
        IPaymentEmployeeUseCase useCase,
        MessageParser parser)
    {
        _publisher = publisher;
        _useCase = useCase;
        _parser = parser;
    }

    public async Task ProcessMessageAsync(byte[] bytes)
    {
        try
        {
            var payment = await _parser.Parse<PaymentEmployeeModel>(bytes);
            await _useCase.AddAsync(payment);
        }
        catch
        {
            await _publisher.ToRetryQueueAsync(new Message { Content = bytes, Attempts = 1 });
        }
    }

    public async Task ProcessBatchMessageAsync(IEnumerable<byte[]> bytes)
    {
        var payments = new List<PaymentEmployeeModel>();
        await Parallel.ForEachAsync(bytes, async (bytes, c) =>
        {
            try
            {
                var payment = await _parser.Parse<PaymentEmployeeModel>(bytes);
                payments.Add(payment);
            }
            catch
            {
                await _publisher.ToRetryQueueAsync(new Message { Content = bytes, Attempts = 1 });
            }
        });
    }

    public async Task ReProcessMessageAsync(Message message)
    {
        throw new NotImplementedException();
    }
}