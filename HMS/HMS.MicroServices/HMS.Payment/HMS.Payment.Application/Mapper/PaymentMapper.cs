using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Enums;

namespace HMS.Payments.Application.Mapper;

public sealed class PaymentMapper
{
    internal Payment Map(PaymentModel model)
    {
        return new Payment
        {
            Amount = model.Amount,
            Beneficiary = model.Beneficiary,
            BeneficiaryCNPJ = model.BeneficiaryCNPJ,
            BeneficiaryCPF = model.BeneficiaryCPF,
            Description = model.Description,
            Observations = model.Observations,
            Payer = model.Payer,
            PayerCNPJ = model.PayerCNPJ,
            PayerCPF = model.PayerCPF,
            PaymentMethod = GetPaymentMethod(model.PaymentMethod)
        };
    }

    internal Payment Map(PaymentUpdateModel model)
    {
        return new Payment
        {
            Amount = model.Amount,
            Beneficiary = model.Beneficiary,
            BeneficiaryCNPJ = model.BeneficiaryCNPJ,
            BeneficiaryCPF = model.BeneficiaryCPF,
            Description = model.Description,
            Observations = model.Observations,
            Payer = model.Payer,
            PayerCNPJ = model.PayerCNPJ,
            PayerCPF = model.PayerCPF,
            PaymentMethod = GetPaymentMethod(model.PaymentMethod),
            ID = model.ID
        };
    }

    internal PaymentOutput Map(Payment entity)
    {
        return new PaymentOutput
        {
            Amount = entity.Amount,
            Beneficiary = entity.Beneficiary,
            BeneficiaryCNPJ = entity.BeneficiaryCNPJ,
            BeneficiaryCPF = entity.BeneficiaryCPF,
            Description = entity.Description,
            ID = entity.ID,
            Observations = entity.Observations,
            Payer = entity.Payer,
            PayerCNPJ = entity.PayerCNPJ,
            PayerCPF = entity.PayerCPF,
            PaymentMethod = entity.PaymentMethod.ToString(),
        };
    }

    internal List<PaymentOutput> Map(List<Payment> entities)
    {
        return entities.Select(x => Map(x)).ToList();
    }

    private PaymentMethodEnum GetPaymentMethod(string method)
    {
        if(Enum.TryParse(method, out PaymentMethodEnum methodEnum))
        {
            return methodEnum;
        }
        else
        {
            return default;
        }
    }
}