using HMS.ContractsMicroService.Core.Json;
using System.Collections;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HMS.ContractsMicroService.Core.Extensions
{
    public static class ObjectExtensions
    {
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



        internal static TEntity Replacer<TEntity>(this TEntity obj, TEntity valuesToReplace) where TEntity : notnull
        {
            obj.GetType().GetProperties()
                .CustomForEach((property, index) =>
                {
                    var valueIsValid = valuesToReplace.GetType().GetProperty(property.Name)
                    .TryGetValue(obj, out var result);
                    if(valueIsValid == false || result == default) return;
                    property.SetValue(obj, result);
                });
            return obj;
        }

        internal static void CustomForEach<T>(this T[] array, Action<T, int> action)
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

        internal static void CustomForEach<T>(this T[] array, Action<T, int, CancellationTokenSource> action)
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
        public static TTarget FromTo<TTarget>(this object obj) where TTarget : new()
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            //var target = new TTarget();

            var objType = obj.GetType();
            var targetType = typeof(TTarget);
            var target = (TTarget)obj.FromTo(targetType);
            if (target == null)
                throw new ArgumentNullException("Erro null");
            return target;
            //return target;
        }
        public static object FromTo(this object obj, Type type)
        {
            var target = Activator.CreateInstance(type)
                ?? throw new Exception("Null");
            obj .GetType() //
                .GetProperties()
                .CustomForEach((prop, index) =>
            {
                var propTarget = type.GetProperty(prop.Name);
                if (propTarget == null) return;
                var valueIsValid = prop.TryGetValue(obj, out var result);
                if (valueIsValid == false) return;
                var a = result ?? throw new Exception("prop ressult \n \n \n");
                if (result.GetType() != propTarget.PropertyType) //
                {
                    if (propTarget.PropertyType.IsEnum)
                        result = result.ChangeTypeToEnum(propTarget.PropertyType);
                    else if (propTarget.PropertyType.IsEnumerable() && prop.PropertyType.IsEnumerable())
                        result = result.ChangeListToListEnum(propTarget.PropertyType);
                    else if (propTarget.PropertyType.IsClass)
                        result = result.FromTo(propTarget.PropertyType);
                }
                propTarget.SetValue(target, result);
            });
            //Console.WriteLine(/*JsonManipulation.Serialize(target).Result + "Bataa" + */$"{value.GetType().Name} {target.GetType().Name}");
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
