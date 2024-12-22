using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Enums;

namespace HMS.Payments.Application.Mapper;

public sealed class EmployeePaymentMapper
{
    internal PaymentEmployee Map(PaymentEmployeeUpdateModel model)
    {
        return new PaymentEmployee
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
            PaymentMethod = GetPaymentMethod(model.PaymentMethod),
            TotalAmountOfBenefits = model.TotalAmountOfBenefits
        };
    }

    internal async Task<List<PaymentEmployee>> Map(List<PaymentEmployeeModel> models)
    {
        var employees = new List<PaymentEmployee>();
        foreach (var model in models) employees.Add(Map(model));

        return employees;
    }

    internal PaymentEmployee Map(PaymentEmployeeModel model)
    {
        return new PaymentEmployee
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
            PaymentMethod = GetPaymentMethod(model.PaymentMethod),
            TotalAmountOfBenefits = model.TotalAmountOfBenefits
        };
    }

    internal PaymentEmployeeOutput Map(PaymentEmployee entity)
    {
        return new PaymentEmployeeOutput
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
            PaymentMethod = entity.PaymentMethod.ToString(),
            TotalAmountOfBenefits = entity.TotalAmountOfBenefits
        };
    }

    internal List<PaymentEmployeeOutput> Map(List<PaymentEmployee> entity)
    {
        return entity.Select(x => Map(x)).ToList();
    }

    private PaymentMethodEnum GetPaymentMethod(string method)
    {
        if (Enum.TryParse(method, out PaymentMethodEnum methodEnum)) return methodEnum;

        return default;
    }
}