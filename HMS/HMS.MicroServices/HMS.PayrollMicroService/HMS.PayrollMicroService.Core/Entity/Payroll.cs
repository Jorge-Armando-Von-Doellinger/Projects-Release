using HMS.PayrollMicroService.Core.Data.Discounts;
using HMS.PayrollMicroService.Core.Entity.Base;
using HMS.PayrollMicroService.Core.Enums;
using HMS.PayrollMicroService.Core.Mapper;

namespace HMS.PayrollMicroService.Core.Entity
{
    public sealed class Payroll : EntityBase
    {
        public Payroll()
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

        public void Update(Payroll payroll)
        {
            base.Update();
            this.Replacer<Payroll, EntityBase>(payroll); // Repassa propriedades de um objeto para o outro, desde que os nomes, tipos e valores sejam iguais e diferentes de null
        }
    }
}
