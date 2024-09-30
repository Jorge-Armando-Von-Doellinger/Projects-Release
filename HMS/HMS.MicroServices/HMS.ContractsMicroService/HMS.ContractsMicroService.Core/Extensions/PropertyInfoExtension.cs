using HMS.ContractsMicroService.Core.Enums;
using System.Collections;
using System.Reflection;

namespace HMS.ContractsMicroService.Core.Extensions
{
    public static class PropertyInfoExtension
    {
        internal static bool TryGetValue(this PropertyInfo propertyInfo, object obj, out object? result)
        {
            result = null;
            try
            {
                if (propertyInfo == null) return false;
                if(propertyInfo.CanWrite == false) return false;
                var value = propertyInfo.GetValue(obj);
                if (value == null) return false;

                var valueIsEqualsOrDefault = Equals(value, GetDefaultValue(propertyInfo)) 
                    || value.GetType().IsEnum == false;
                var valueIsListAndDefault = propertyInfo.PropertyType.IsList() &&
                    ListExtensions.SequenceEquals((IList)value, (IList) GetDefaultValue(propertyInfo));
                
                var isValid = value switch
                {
                    null => false,
                    not null => valueIsEqualsOrDefault == false || valueIsListAndDefault == false
                    // not null => verifica se são default
                    // se forem Enums, serão ignorados, mas se forem List<Enum>, serão lidos e conferidos!
                };
                result = isValid ? value : null;
                return isValid;
            }
            catch
            {
                return false;
            }
            
        }
        internal static object GetDefaultValue(this PropertyInfo prop)
        {
            if (prop.CanRead)
                return prop.GetValue(Activator.CreateInstance(prop.DeclaringType));
            Console.WriteLine(prop.DeclaringType + "    type");
            Console.WriteLine("\n Null");
            return null;
        }
    }
}
