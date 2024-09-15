using HMS.PayrollMicroService.Core.Enums;

namespace HMS.PayrollMicroService.Core.Data.Discounts
{
    public sealed class Discount
    {
        public Discount()
        {
            MandatoryDiscounts = Enum.GetValues(typeof(MandatoryDiscountsEnum))
                .Cast<MandatoryDiscountsEnum>()
                .ToList();
        }
        // public Dictionary<MandatoryDiscountsEnum, int> MandatoryDiscountsPercentage { get; set; } 
        // - Talvez futuramente
        public List<MandatoryDiscountsEnum> MandatoryDiscounts { get; set; }
        public int TotalDiscounts { get; set; }
        public List<string>? OtherDiscounts { get; set; }
    }
}
