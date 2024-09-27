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
                if(value == GetDefaultValue(propertyInfo)) return false;
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
            return null;
        }
    }
}
