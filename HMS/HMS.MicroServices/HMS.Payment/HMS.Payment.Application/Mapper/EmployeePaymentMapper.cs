using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Entity;

namespace HMS.Payments.Application.Mapper
{
    public sealed class EmployeePaymentMapper
    {
        internal PaymentEmployee Map(PaymentEmployeeUpdateModel model)
        {
            return new()
            {
                Amount = model.Amount,
                Beneficiary = model.Beneficiary,
                BeneficiaryCNPJ = model.BeneficiaryCNPJ,
                BeneficiaryCPF = model.BeneficiaryCPF,
                Benefits = model.Benefits,
                Description = model.Description,
                EmployeeId = model.EmployeeId,
                HourlySalary = model.HourlySalary,
                HoursWorkedInMonth = model.HoursWorkedInMonth,
                ID = model.ID,
                Observations = model.Observations,
                Payer = model.Payer,
                PayerCNPJ = model.PayerCNPJ,
                PayerCPF = model.PayerCPF,
                PaymentMethod = model.PaymentMethod,
                TotalAmountOfBenefits = model.TotalAmountOfBenefits,
            };
        }
        internal PaymentEmployee Map(PaymentEmployeeModel model)
        {
            return new()
            {
                Amount = model.Amount,
                Beneficiary = model.Beneficiary,
                BeneficiaryCNPJ = model.BeneficiaryCNPJ,
                BeneficiaryCPF = model.BeneficiaryCPF,
                Benefits = model.Benefits,
                Description = model.Description,
                EmployeeId = model.EmployeeId,
                HourlySalary = model.HourlySalary,
                HoursWorkedInMonth = model.HoursWorkedInMonth,
                Observations = model.Observations,
                Payer = model.Payer,
                PayerCNPJ = model.PayerCNPJ,
                PayerCPF = model.PayerCPF,
                PaymentMethod = model.PaymentMethod,
                TotalAmountOfBenefits = model.TotalAmountOfBenefits,
            };
        }
        internal PaymentEmployeeOutput Map(PaymentEmployee entity)
        {
            return new()
            {
                Amount = entity.Amount,
                Beneficiary = entity.Beneficiary,
                BeneficiaryCNPJ = entity.BeneficiaryCNPJ,
                BeneficiaryCPF = entity.BeneficiaryCPF,
                Benefits = entity.Benefits,
                Description = entity.Description,
                EmployeeId = entity.EmployeeId,
                HourlySalary = entity.HourlySalary,
                HoursWorkedInMonth = entity.HoursWorkedInMonth,
                Observations = entity.Observations,
                Payer = entity.Payer,
                PayerCNPJ = entity.PayerCNPJ,
                PayerCPF = entity.PayerCPF,
                PaymentMethod = entity.PaymentMethod,
                TotalAmountOfBenefits = entity.TotalAmountOfBenefits
            };
        }
        internal List<PaymentEmployeeOutput> Map(List<PaymentEmployee> entity)
        {
            return entity.Select(x => Map(x)).ToList();
        }
    }
}
