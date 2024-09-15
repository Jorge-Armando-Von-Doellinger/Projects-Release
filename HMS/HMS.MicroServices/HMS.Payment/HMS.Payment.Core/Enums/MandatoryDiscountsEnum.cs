using System.Text.Json.Serialization;

namespace HMS.Payments.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MandatoryDiscountsEnum
    {
        INSS, 
        FGTS,
        IRRF
    }
}
