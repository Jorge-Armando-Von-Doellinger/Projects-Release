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
                var _ = value switch
                {
                    null => false,
                    not null => Equals(value, GetDefaultValue(propertyInfo)) || // Verifica se é igual, caso seja um tipo de valor (int,string, etc)
                    ( (propertyInfo.PropertyType.IsList() ) && 
                    ListExtensions.SequenceEquals((IList)value, (IList)GetDefaultValue(propertyInfo))),
                    //not null => false
                    // Verifica se é Enumeravel e se são iguais
                };
                if(_ == true) return false;
/*                if(Equals(value, GetDefaultValue(propertyInfo)))
                    return false;
                if(propertyInfo.PropertyType.IsEnumerable())
                    if(ListExtensions.SequenceEquals((IList) value, (IList) GetDefaultValue(propertyInfo)))
                        return false;*/

                result = value;
                return true;
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
