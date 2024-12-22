using System.Text;
using HMS.Payments.Application.Interfaces.UseCases;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Parsers;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Processor;

namespace HMS.Payments.Application.Processor;

public class RefundProcessor : IMessageProcessor
{
    private readonly MessageParser _parser;
    private readonly IMessagePublisher _publisher;
    private readonly IRefundUseCase _useCase;

    public RefundProcessor(IMessagePublisher publisher,
        IRefundUseCase useCase,
        MessageParser parser)
    {
        _publisher = publisher;
        _useCase = useCase;
        _parser = parser;
    }

    public async Task ProcessMessageAsync(byte[] bytes)
    {
        var json = Encoding.UTF8.GetString(bytes);
        try
        {
            var refund = await _parser.Parse<RefundModel>(bytes);
            await _useCase.AddAsync(refund);
        }
        catch
        {
            await _publisher.ToRetryQueueAsync(new Message { Content = bytes, Attempts = 1 });
        }
    }

    public async Task ProcessBatchMessageAsync(IEnumerable<byte[]> bytes)
    {
        var refunds = new List<RefundModel>();
        await Parallel.ForEachAsync(bytes, async (message, c) =>
        {
            try
            {
                var refund = await _parser.Parse<RefundModel>(message);
                refunds.Add(refund);
            }
            catch
            {
                await _publisher.ToRetryQueueAsync(new Message { Content = message, Attempts = 1 });
            }
        });
    }

    public async Task ReProcessMessageAsync(Message message)
    {
        throw new NotImplementedException();
    }
}