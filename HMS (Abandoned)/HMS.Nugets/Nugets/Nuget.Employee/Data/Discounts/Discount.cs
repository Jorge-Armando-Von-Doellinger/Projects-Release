using Nuget.Employee.Enum;

namespace Nuget.Employee.Data.Discounts
{
    
    public sealed class Discount
    {
        // public Dictionary<MandatoryDiscountsEnum, int> MandatoryDiscountsPercentage { get; set; } 
        // - Talvez futuramente
        public List<MandatoryDiscountsEnum> MandatoryDiscounts { get; set; }
        public int TotalDiscounts { get; set; }
        public List<string>? OtherDiscounts { get; set; }
    }
}
