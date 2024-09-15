using Nuget.Payroll.Data.Discounts;

namespace Nuget.Payroll.Input
{
    public sealed class PayrollInput
    {
        public int HourlySalary { get; set; }
        public short HoursWorkedInMonth { get; set; }
        public List<string> Benefits { get; set; }
        public int TotalAmountOfBenefits { get; set; }
        public Guid EmployeeId { get; set; }
        public Discount Discounts { get; set; }
    }
}
