using System.Collections;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Runtime.Serialization;

namespace HMS.ContractsMicroService.Core.Extensions
{
    public static class ObjectExtensions
    {
        // Publics or Internal
        public static bool HaveAPropertyDefault<TObject>(this TObject obj, out List<string> nameOfPropertiesDefault)
        {
            // TypeOf não funciona, devido ser um type object no ValidateModelAttribute
            var defaultInstance = Activator.CreateInstance(obj.GetType());
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var nameOfDefaults = new List<string>();

            foreach (var prop in properties)
            {

                var valueGetSuccess = prop.TryGetValue(obj, out var value);
                if (valueGetSuccess == false) continue;
                var currentValue = prop.GetValue(obj);
                var defaultValue = prop.GetValue(defaultInstance);

                if (Equals(currentValue, defaultValue))
                    if (prop.PropertyType.IsEnum == false)
                        nameOfDefaults.Add(prop.Name);
            }
            nameOfPropertiesDefault = nameOfDefaults;
            return nameOfDefaults.Count > 0;
        }

        public static TObject[] FromTo<TObject>(this object[] objArray)
        {
            var targetItemType = typeof(TObject);
            var target = Array.CreateInstance(targetItemType, objArray.Length);
            int count = 0;
            foreach (var item in objArray)
            {
                var obj = item.FromTo(targetItemType);
                target.SetValue(obj, count);
                count++;
            }
            return (TObject[]) target;
        }

        internal static void Replacer<TEntity>(this TEntity obj, TEntity valuesToReplace, bool ignoreInheritedValues) 
            where TEntity : class
        {
            if(obj == null) return;
            foreach (var property in obj.GetType().GetProperties())
            {
                var replaceProp = valuesToReplace.GetType().GetProperty(property.Name);
                if (replaceProp == null) continue;
                if (replaceProp.DeclaringType != obj.GetType() && ignoreInheritedValues) continue;
                var valueIsValid = replaceProp.TryGetValue(valuesToReplace, out var result);
                if (valueIsValid == false) continue;
                property.SetValue(obj, result);
            };
        }

        public static TTarget FromTo<TTarget>(this object obj, Type? typeToIgnore = null) where TTarget : new()
        {
            ArgumentNullException.ThrowIfNull(obj);
            var objType = obj.GetType();
            var targetType = typeof(TTarget);
            var target = (TTarget)obj.FromTo(targetType, typeToIgnore);
            ArgumentNullException.ThrowIfNull(target);
            return target;

        }
        
        public static object FromTo(this object obj, Type type, Type? typeToIgnore = null)
        {
            var target = Activator.CreateInstance(type) ?? throw new Exception("Null");
            foreach(var prop in obj.GetType().GetProperties())
            {
                var propTarget = type.GetProperty(prop.Name);
                if (propTarget == null) continue;
                
                if (propTarget.DeclaringType == typeToIgnore) 
                {
                    continue;
                }   
                var valueIsValid = prop.TryGetValue(obj, out var result);
                if (valueIsValid == false) continue;
                if (result?.GetType().GetProperty(propTarget.Name)?.DeclaringType?.BaseType == typeToIgnore) continue;
                if (prop.PropertyType != propTarget.PropertyType)
                    result = ChangeType(result, propTarget.PropertyType, out var result1) ? result1 : result;
                propTarget.SetValue(target, result);
            };
            return target;
        }

        // Privates

        private static void CustomForEach<T>(this T[] array, Action<T, int, CancellationTokenSource> action)
        {
            try
            {
                var token = new CancellationTokenSource();
                
                for (int i = 0; i < array.Length; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Canewlado?");
                        break;
                    }
                    action(array[i], i, token);
                }

            }
            catch
            {
                throw;
            }
        }
        
        private static void CustomForEach<T>(this T[] array, Action<T, int> action)
        {
            try
            {
                for (int i = 0; i < array.Length ; i++)
                {
                    action(array[i], i);
                }

            }
            catch
            {
                throw;
            }
        }

        private static bool ChangeType(this object obj, Type destine, out object result)
        {
            result = null;
            if(destine.IsEnum) result = obj.ChangeTypeToEnum(destine);
            else if (obj.GetType().IsNumberEnumerable() && destine.IsEnumEnumerable()) result = obj.ChangeListToListEnum(destine);
            else if (obj.GetType().IsEnum && destine == typeof(string)) result = obj.ToString();
            else if (obj.GetType().IsEnumEnumerable() && destine == typeof(string)) result = obj.ListEnumFromString();
            else if(obj.GetType().IsClass && destine.IsClass) result = obj.FromTo(destine);
            else return false;
            return true;

        }

        private static object ChangeListToListEnum(this object value, Type targetType)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (targetType.IsEnumEnumerable())
            {
                var itemType = targetType.GetGenericArguments()[0];
                if (itemType.IsEnum == false) throw new InvalidOperationException($"Target type {targetType} is not a list of enums.");
                //var typeList = typeof(List<>).MakeGenericType(itemType);
                var targetList = itemType.MakeGenericList();

                if (value is IEnumerable sourceList)
                    foreach (var item in sourceList)                
                        AddValueToListEnum(item, itemType, targetList);
                return targetList;

            }
            throw new InvalidOperationException($"Target type {targetType} is not a list type.");
        }

        private static void AddValueToListEnum(this object value, Type itemType, IList targetList)
        {
            if (value.GetType() == typeof(string))
                targetList.Add(ConvertValue(value, itemType));
            else if (value.TryParseToInt32(out int result))
            {
                if (Enum.IsDefined(itemType, result))
                    targetList.Add(Enum.ToObject(itemType, result));
            }
            else
                throw new InvalidOperationException($"Invalid type {value.GetType()} for conversion to {itemType}");
        }
        
        private static object ConvertValue(object value, Type typeTarget)
        {
            if(typeTarget.IsEnum)
                return value.ChangeTypeToEnum(typeTarget);
            if (typeTarget.IsGenericType && typeof(List<>).IsAssignableFrom(typeTarget.GetGenericTypeDefinition()))
                return value.ChangeListToListEnum(typeTarget);
            throw new InvalidOperationException("Não foi possivel converter este value");
        }

        private static object ChangeTypeToEnum(this object obj, Type type)
        {
            if (type.IsEnum == false)
                throw new InvalidDataException("Type invalid");
            if (obj.GetType().IsEnumerable() && type.IsEnumerable())
                return ChangeListToListEnum(obj, type);
            if (obj.GetType() == typeof(string))
            {
                if (Enum.TryParse(type, obj.ToString(), out var result))
                    return result;
            }
            else if(obj.TryParseToInt32(out var value))
                if(Enum.IsDefined(type, value))
                    return Enum.ToObject(type, value);
                else
                    return null;
            //return default;
            throw new Exception("Object and type destine isn't compatible");
        }

        private static object ListEnumFromString(this object listEnum)
        {
            if (!listEnum.GetType().IsEnumerable() && !listEnum.GetType().IsEnumEnumerable())
                return default;
            var list = new List<string>();
            foreach (var itemEnum in (IList)listEnum)
            {
                list.Add(itemEnum.ToString());
            }
            return string.Join(", ", list);
        }

        private static bool TryParseToInt32(this object obj, out int value)
        {
            try
            {
                value = Convert.ToInt32(obj);
                return true;
            }
            catch
            {
                value = 0;
                return false;
            }
        }
    }
}
