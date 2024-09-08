using HMS.Employee.Core.Data.Discounts;
using HMS.Employee.Core.Entity.Base;
using HMS.Employee.Core.Enum;
using HMS.Employee.Core.Mapper;

namespace HMS.Employee.Core.Entity
{
    public sealed class Payroll : BaseEntityWithEmployeeId
    {
        public int HourlySalary { get; set; }
        public short HoursWorkedInMonth { get; set; }
        public int TotalAmountOfBenefits { get; set; }
        public List<BenefitsEnum>? Benefits { get; set; }
        public Discount Discounts { get; set; }
        public ContractualInformation ContractualInformation { get; set; }
        public Guid ContractId { get; set; }

        public void Update(Payroll payroll)
        {
            base.Update();
            ObjectExtension.Replacer(this, payroll);
        }
    }
}
