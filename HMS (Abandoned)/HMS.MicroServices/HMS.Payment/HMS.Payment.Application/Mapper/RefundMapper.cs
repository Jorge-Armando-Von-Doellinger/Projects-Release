using HMS.Payments.Application.Models.Input;
using HMS.Payments.Core.Entity;

namespace HMS.Payments.Application.Mapper;

public sealed class RefundMapper
{
    internal IEnumerable<Refund> Map(IEnumerable<RefundModel> refunds)
    {
        return refunds.Select(Map);
    }

    internal Refund Map(RefundModel refund)
    {
        return new Refund
        {
            Amount = refund.Amount,
            Reason = refund.Reason,
            PaymentId = refund.PaymentId
        };
    }
}