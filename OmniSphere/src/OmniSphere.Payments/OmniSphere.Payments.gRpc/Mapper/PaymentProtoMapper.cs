using OmniSphere.Payments.Application.DTOs;
using OmniSphere.Payments.Core.Entity;
using OmniSphere.Payments.Core.Enums;

namespace OmniSphere.Payments.gRpc.Mapper;

public class PaymentProtoMapper
{
    public PaymentDTO MapToPaymentDTO(PaymentProto payment)
    {
        return new()
        {
            Amount = payment.Amount,
            Message = payment.Message,
            AccountId = payment.AccountId,
        };
    }

    public PaymentProto MapToPaymentProto(PaymentDTO payment)
    {
        return new()
        {
            Amount = payment.Amount,
            Message = payment.Message,
            AccountId = payment.AccountId,
        };
    }

    public IEnumerable<PaymentProto> MapToListPaymentDTOs(IEnumerable<PaymentDTO> payments)
    {
        return payments.Select(MapToPaymentProto);
    }
    
    public ListPaymentsWithMessageProto MapToListPaymentsWithMessageProto(IEnumerable<PaymentProto> payments)
    {
        var listPaymentsWithMessageProto = new ListPaymentsWithMessageProto();
        listPaymentsWithMessageProto.Payments.AddRange(payments);
        return listPaymentsWithMessageProto;
    }
}