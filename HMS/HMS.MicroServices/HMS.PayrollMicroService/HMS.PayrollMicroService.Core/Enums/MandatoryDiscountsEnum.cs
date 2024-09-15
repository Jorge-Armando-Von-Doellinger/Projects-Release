using System.Text.Json.Serialization;

namespace HMS.PayrollMicroService.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MandatoryDiscountsEnum
    {
        INSS, 
        FGTS
    }
}
