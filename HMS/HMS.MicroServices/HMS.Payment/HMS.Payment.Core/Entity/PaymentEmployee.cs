using HMS.Payments.Core.Enums;
using HMS.Payments.Core.Exceptions;

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
        public List<string> Benefits { get; set; }
        public decimal TotalAmountOfBenefits { get; set; } // Virá do contrato


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
        public override void ValidateEntity()
        {
            base.ValidateEntity();
            ValidateBenefits();
            ValidateIds();
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

        private void ValidateBenefits()
        {
            // Depois será atualizado para estes dados sairem do contrato!
            if (Benefits == null || Benefits.Count == 0) return;
            if(!Benefits.All(x => Enum.IsDefined(typeof(BenefitsEnum), x))) throw new ArgumentOutOfRangeException(nameof(Benefits), "Beneficios do funcionario são inválido!");
            if(TotalAmountOfBenefits <= 0) throw new PaymentInvalidException("Valor minimo dos beneficios não foram cumpridos");
        }

        private void ValidateIds()
        {
            if(string.IsNullOrWhiteSpace(EmployeeId)) throw new ArgumentNullException(nameof(EmployeeId), "Identificador do funcionario não pode ser null");
        }
    }
}
