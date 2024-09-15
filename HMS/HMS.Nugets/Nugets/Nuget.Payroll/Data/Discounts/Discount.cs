namespace Nuget.Payroll.Data.Discounts
{
    public sealed class Discount
    {
        // public Dictionary<MandatoryDiscountsEnum, int> MandatoryDiscountsPercentage { get; set; } 
        // - Talvez futuramente
        public List<string>? OtherDiscounts { get; set; }
        public int TotalDiscounts { get; set; }
    }
}
