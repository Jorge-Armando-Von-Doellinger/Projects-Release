using HMS.ContractsMicroService.Core.Json;
using System.Collections;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HMS.ContractsMicroService.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static TTarget FromTo<TTarget>(this object obj) where TTarget : new ()
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            //var target = new TTarget();

            var objType = obj.GetType();
            var targetType = typeof(TTarget);
            var target = (TTarget)obj.FromTo(targetType);
            if(target == null)
                throw new ArgumentNullException("Erro null");
            return target;        
            //return target;
        }
        public static object FromTo(this object obj, Type type)
        {
            var target = Activator.CreateInstance(type);
            var objType = obj.GetType();
            foreach (var prop in objType.GetProperties())
            {
                var propTarget = type.GetProperty(prop.Name);
                if (propTarget == null || propTarget.CanWrite == false) continue;
                var value = prop.GetValue(obj);
                if (value == null) continue;
                if (value.GetType() != propTarget.PropertyType)
                {
                    if (propTarget.PropertyType.IsEnum)
                        value = value.ChangeTypeToEnum(propTarget.PropertyType);
                    else if(propTarget.PropertyType.IsEnumerable() && prop.PropertyType.IsEnumerable())
                        value = value.ChangeListToListEnum(propTarget.PropertyType);
                    else if(propTarget.PropertyType.IsClass)
                        value = value.FromTo(propTarget.PropertyType);
                }
                propTarget.SetValue(target, value);
                //Console.WriteLine(/*JsonManipulation.Serialize(target).Result + "Bataa" + */$"{value.GetType().Name} {target.GetType().Name}");
            }
            return target;
        }

        private static object ChangeListToListEnum(this object value, Type targetType)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (targetType.IsGenericType && typeof(List<>).IsAssignableFrom(targetType.GetGenericTypeDefinition()))
            {
                var itemType = targetType.GetGenericArguments()[0];
                if (itemType.IsEnum == false)
                    throw new InvalidOperationException($"Target type {targetType} is not a list of enums.");
                var typeList = typeof(List<>).MakeGenericType(itemType);
                var targetList = (IList)Activator.CreateInstance(typeList);

                if (value is IEnumerable sourceList)
                {
                    foreach (var item in sourceList)
                    {
                        if (item.GetType() == typeof(string) || item.TryParseToInt32(out var result))
                            targetList.Add(ConvertValue(item, itemType));
                        else
                            throw new InvalidOperationException($"Invalid type {item.GetType()} for conversion to {itemType}");
                    }
                }
                return targetList;

            }
            throw new InvalidOperationException($"Target type {targetType} is not a list type.");
        }

        public static object ConvertValue(object value, Type typeTarget)
        {
            Console.WriteLine(typeTarget);
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
                Console.WriteLine("Teste \n \n \n");
            }
            else if(obj.TryParseToInt32(out var value))
                if(Enum.IsDefined(type, value))
                    return Enum.ToObject(type, value);
                else
                    return null;
            //return default;
            throw new Exception("Object and type destine isn't compatible");
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
