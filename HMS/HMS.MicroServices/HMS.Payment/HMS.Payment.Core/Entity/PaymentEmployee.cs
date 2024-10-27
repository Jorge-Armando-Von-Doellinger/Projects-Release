using HMS.Payments.Core.Enums;

namespace HMS.Payments.Core.Entity
{
    public sealed class PaymentEmployee : Payment
    {
        public PaymentEmployee()
        {
            SetMandatoryBenefits();
        }
        public string EmployeeId { get; set; } // Quando bater dia 5 ou dia 20, será buscado pelos empregados, onde serão capturados os IDs e realizará o pagamento com base nos dados apresentados!
        public int HourlySalary { get; set; } // Virá do contrato
        public short HoursWorkedInMonth { get; set; } // Virá do contrato
        private List<string> _benefits;
        public List<string> Benefits
        {
            get => _benefits;
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                if (value.All(x => Enum.IsDefined(typeof(BenefitsEnum), x)))
                {
                    _benefits = new (value);
                    SetMandatoryBenefits();
                }
                else throw new ArgumentOutOfRangeException(nameof(value), "Metodo de pagamento inválido!");
            }
        }
        public int TotalAmountOfBenefits { get; set; } // Virá do contrato


        public void SetMandatoryBenefits()
        {
            if (Benefits == null) Benefits = new();
            var mandatoryBenefits = Enum.GetValues(typeof(BenefitsEnum))
                .Cast<BenefitsEnum>()
                .Take(10)
                .ToList()
                .Select(x => x.ToString())
                .Where(x => !Benefits.Contains(x));
            Benefits.AddRange(mandatoryBenefits);
        }

        public void Update(PaymentEmployee payroll)
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
