using HMS.Payments.Core.Data.Discounts;
using HMS.Payments.Core.Entity.Base;
using HMS.Payments.Core.Enums;
using HMS.Payments.Core.Mapper;

namespace HMS.Payments.Core.Entity
{
    public sealed class EmployeePayment : EntityBase
    {
        public EmployeePayment()
        {
            SetMandatoryBenefits();
        }
        public Guid EmployeeId { get; set; }
        public int HourlySalary { get; set; } // Virá do contrato
        public short HoursWorkedInMonth { get; set; } // Virá do contrato, mas calculado junto com o mensal do funcionario
        public List<BenefitsEnum> Benefits { get; set; } = new(); // Virá do contrato
        public int TotalAmountOfBenefits { get; set; } // Virá do contrato
        public Discount Discounts { get; set; } = new(); // Virá do contrato

        private void SetMandatoryBenefits()
        {
            if(Benefits == null)
                Benefits = new List<BenefitsEnum>();
            var mandatoryBenefits = Enum.GetValues(typeof(BenefitsEnum))
                .Cast<BenefitsEnum>()
                .Take(10)
                .ToList();
            Benefits.AddRange(mandatoryBenefits);
            Console.WriteLine("Teste");
        }

        public void Update(EmployeePayment payroll)
        {
            base.Update();
            this.Replacer<EmployeePayment, EntityBase>(payroll); // Repassa propriedades de um objeto para o outro, desde que os nomes, tipos e valores sejam iguais e diferentes de null
        }
    }
}
