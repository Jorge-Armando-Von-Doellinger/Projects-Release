using HMS.Payments.Core.Json;

namespace HMS.Payments.Core.Extensions
{
    public static class TypeExtension
    {
        public static bool IsTypeEnumerable(this Type type)
        {
            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                var interfaces = type.GetInterfaces();
                bool isGenericCollectionInterface = interfaces.Any(i =>
                     i.IsGenericType &&
                     (i.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                      i.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                      i.GetGenericTypeDefinition() == typeof(IList<>)));
                return isGenericCollectionInterface;
            }
            return false;
        }

        public static bool IsEnumEnumerable(this Type type)
        {
            return type.GetGenericArguments().Any(x => x.IsEnum)
                && type.IsTypeEnumerable();
        }
    }
}
