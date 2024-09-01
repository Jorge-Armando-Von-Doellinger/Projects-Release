using Nuget.Employee.Data.Discounts;
using Nuget.Employee.Enum;
using Nuget.Employee.Inputs.Base;

namespace Nuget.Employee.Inputs
{
    public class PayrollInput : BaseWithEmployeeId
    {
        public int HourlySalary { get; set; }
        public short HoursWorkedInMonth { get; set; }
        public List<BenefitsEnum> Benefits { get; set; }
        public int TotalAmountOfBenefits { get; set; }
        public Discount Discounts { get; set; }
    }
}
