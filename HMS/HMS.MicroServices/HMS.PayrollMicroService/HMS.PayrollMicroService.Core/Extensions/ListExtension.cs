using HMS.PayrollMicroService.Core.Enums;
using HMS.PayrollMicroService.Core.Mapper;
using System.Collections;

namespace HMS.PayrollMicroService.Core.Extensions
{
    public static class ListExtension
    {
        public static List<SingleOut> Map<SingleOut>(this IList list) where SingleOut : new ()
        {
            var listOut = new List<SingleOut>();
            foreach (object item in list)
            {
                var value = item.FromObjectTo<SingleOut>();
                listOut.Add(value);
            }
            return listOut;
        }
        public static List<TypeEnum> ToListEnum<TypeEnum>(this IList<string> list)
        {
            var data = list
                .Select(s => (TypeEnum)Enum.Parse(typeof(BenefitsEnum), s))
                .ToList();
            return data;
        }
        public static List<TypeEnum> ToListEnum<TypeEnum>(this IList<string> list, List<TypeEnum> addRange)
        {
            var data = list
                .Select(s => (TypeEnum)Enum.Parse(typeof(BenefitsEnum), s))
                .ToList();
            data.AddRange(addRange);
            return data;
        }
    }
}
