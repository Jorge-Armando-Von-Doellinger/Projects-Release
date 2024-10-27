using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Entity;

namespace HMS.Payments.Application.Mapper
{
    public sealed class PaymentMapper
    {
        internal Payment Map(PaymentModel model)
        {
            return new()
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
                PaymentMethod = model.PaymentMethod,
            };

        }
        internal Payment Map(PaymentUpdateModel model)
        {
            return new()
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
                PaymentMethod = model.PaymentMethod,
                ID = model.ID,
            };

        }
        internal PaymentOutput Map(Payment entity)
        {
            return new()
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
                PaymentMethod = entity.PaymentMethod,
            };
        }
        internal List<PaymentOutput> Map(List<Payment> entities)
        {
            return entities.Select(x => Map(x)).ToList();
        }

    }
}
