using OmniSphere.Payments.Application.DTOs;
using OmniSphere.Payments.Core.Entity;
using OmniSphere.Payments.Core.Enums;

namespace OmniSphere.Payments.Application.Mappers;

public class PaymentMapper
{
    internal Payment MapToPayment(PaymentDTO dto)
    {
        return new()
        {
            Amount = dto.Amount,
            Message = dto.Message,
            AccountId = dto.AccountId
        };
    }

    internal Payment MapToPayment(PaymentWithStatusAndIdDTO payment)
    {
        return new()
        {
            Amount = payment.Amount,
            Message = payment.Message,
            Status = (PaymentStatusEnum)payment.PaymentStatus,
            AccountId = payment.AccountId,
            Id = payment.PaymentId
        };
    }

    internal PaymentWithStatusAndIdDTO MapToPaymentDTO(Payment payment)
    {
        return new()
        {
            Amount = payment.Amount,
            Message = payment.Message,
            PaymentId = payment.Id,
            AccountId = payment.AccountId,
            PaymentStatus = (int) payment.Status
        };
    }
    internal IEnumerable<PaymentWithStatusAndIdDTO> MapToListPaymentDTO(IEnumerable<Payment> payments) 
        => payments.Select(MapToPaymentDTO);
}