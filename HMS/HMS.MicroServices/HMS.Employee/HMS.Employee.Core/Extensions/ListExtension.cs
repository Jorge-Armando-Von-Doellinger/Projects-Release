using HMS.Employee.Core.Mapper;
using System.Collections;

namespace HMS.Employee.Core.Extensions
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
    }
}
