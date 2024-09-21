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
                if(propertyInfo.CanWrite == false)
                Console.WriteLine($"TRY GET VALUE \n OBJECT: {propertyInfo.Name} CAN RIGHT: {propertyInfo.CanWrite}");
                if (propertyInfo == null) return false;
                var value = propertyInfo.GetValue(obj);
                if (value == null) return false;
                result = value;
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
