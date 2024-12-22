using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Repository;
using HMS.Payments.Infrastructure.Context;

namespace HMS.Payments.Infrastructure.Repository;

public sealed class RefundRepository : IRefundRepository
{
    private readonly MongoContext _context;

    public RefundRepository(MongoContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Refund refund)
    {
        await _context.Refunds.InsertOneAsync(refund);
    }

    public async Task AddBatchAsync(IEnumerable<Refund> refunds)
    {
        await _context.Refunds.InsertManyAsync(refunds);
    }
}