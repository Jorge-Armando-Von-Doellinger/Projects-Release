using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Enums;

namespace HMS.Payments.Core.Entity
{
    public sealed class EmployeePayment : Payments.Core.Entity.Payment
    {
        public EmployeePayment()
        {
            SetMandatoryBenefits();
        }
        public Guid EmployeeId { get; set; } // Quando bater dia 5 ou dia 20, será buscado pelos empregados, onde serão capturados os IDs e realizará o pagamento com base nos dados apresentados!
        public int HourlySalary { get; set; } // Virá do contrato
        public short HoursWorkedInMonth { get; set; } // Virá do contrato
        public List<BenefitsEnum> Benefits { get; set; } = new(); // Virá do contrato
        public int TotalAmountOfBenefits { get; set; } // Virá do contrato
        

        private void SetMandatoryBenefits()
        {
            if (Benefits == null) Benefits = new ();
            var mandatoryBenefits = Enum.GetValues(typeof(BenefitsEnum))
                .Cast<BenefitsEnum>()
                .Take(10)
                .ToList()
                .Where(x =>
                {
                    if (Benefits.FirstOrDefault(p => p.Equals(x)) != null)
                        return false;
                    return true;
                });
            Benefits.AddRange(mandatoryBenefits);
        }

        public void Update(EmployeePayment payroll)
        {
            base.Update();
            //this.Replacer<EmployeePayment>(payroll); // Repassa propriedades de um objeto para o outro, desde que os nomes, tipos e valores sejam iguais e diferentes de null
        }

        public void CalculateAmoount()
        {
            //Discounts.
            base.Amount = (HourlySalary * HoursWorkedInMonth);
        }
    }
}
