using HMS.Employee.Core.Data.Discounts;
using HMS.Employee.Core.Entity.Base;
using HMS.Employee.Core.Enum;

namespace HMS.Employee.Core.Entity
{
    public sealed class Payroll : BaseEntityWithEmployeeId
    {
        public int HourlySalary { get; set; }
        public short HoursWorkedInMonth { get; set; }
        public List<BenefitsEnum> Benefits { get; set; }
        public int TotalAmountOfBenefits { get; set; }
        public Discount Discounts { get; set; }
    }
}
