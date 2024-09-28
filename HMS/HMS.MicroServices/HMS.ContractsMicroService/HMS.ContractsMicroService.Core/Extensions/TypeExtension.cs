using System.Collections;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace HMS.ContractsMicroService.Core.Extensions
{
    public static class TypeExtension
    {
        public static bool IsEnumerable(this Type type)
        {
            return type.GetInterfaces().Any(i => i.IsGenericType &&
                     (i.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                      i.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                      i.GetGenericTypeDefinition() == typeof(IList<>)));
        }

        internal static bool IsList(this Type type)
        {
            return type.GetInterfaces().Any(i => i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IList<>));
        }

        internal static bool IsEnumEnumerable(this Type type)
        {
            var arguments = type.GetGenericArguments();
            return IsEnumerable(type) && (arguments.Length > 0) switch
            {
                true => arguments[0].IsEnum,
                false => false
            };
        }

        internal static bool IsNumberEnumerable(this Type type)
        {
            var arguments = type.GetGenericArguments();
            return IsEnumerable(type) && (arguments.Length > 0) switch
            {
                true => 
                arguments[0] == typeof(sbyte) 
                || arguments[0] == typeof(short) 
                || arguments[0] == typeof(int)
                || arguments[0] == typeof(long),
                false => false
            };
        }

        internal static IList MakeGenericList(this Type type)
        {
            var typeList = typeof(List<>).MakeGenericType(type);
            return (IList) Activator.CreateInstance(typeList);
        }

    }
}
