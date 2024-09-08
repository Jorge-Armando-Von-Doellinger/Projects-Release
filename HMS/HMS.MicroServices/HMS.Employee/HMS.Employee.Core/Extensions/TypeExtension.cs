namespace HMS.Employee.Core.Extensions
{
    public static class TypeExtension
    {
        public static bool? IsTypeEnumerable(this Type type)
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
                return isGenericCollectionInterface ? true : null;
            }
            return false;
        }
    }
}
