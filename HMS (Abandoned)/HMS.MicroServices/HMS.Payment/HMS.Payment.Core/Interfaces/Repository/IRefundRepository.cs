using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Repository;

public interface IRefundRepository
{
    Task AddAsync(Refund refund);
    Task AddBatchAsync(IEnumerable<Refund> refunds);
}