﻿using System.Collections;

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
        public static bool IsEnumEnumerable(this Type type)
        {
            return IsEnumerable(type) && type.GetGenericArguments()[0].IsEnum;
        }
    }
}
