using HMS.Payments.Application.Models.Input;

namespace HMS.Payments.Application.Interfaces.UseCases;

public interface IRefundUseCase
{
    Task AddAsync(RefundModel refund);
    Task AddBatchAsync(IEnumerable<RefundModel> refunds);
}