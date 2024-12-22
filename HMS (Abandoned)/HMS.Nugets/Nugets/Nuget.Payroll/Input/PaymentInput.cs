using Nuget.Payment.Data.Discounts;

namespace Nuget.Payment.Input
{
    public sealed class PaymentInput
    {
        public int HourlySalary { get; set; }
        public short HoursWorkedInMonth { get; set; }
        public List<string> Benefits { get; set; }
        public int TotalAmountOfBenefits { get; set; }
        public Guid EmployeeId { get; set; }
        public Discount Discounts { get; set; }
    }
}
