using HMS.ContractsMicroService.Core.Json;
using System.Collections;
using System.Net.WebSockets;

namespace HMS.ContractsMicroService.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static TTarget FromTo<TTarget>(this object obj) where TTarget : new ()
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var target = new TTarget();

            var objType = obj.GetType();
            var targetType = typeof(TTarget);
            foreach (var prop in objType.GetProperties())
            {
                var propTarget = targetType.GetProperty(prop.Name);
                if (propTarget == null || propTarget.CanWrite == false) continue;
                var value = prop.GetValue(obj);
                if (value == null) continue;
                // REPASSAR PARA PEGAR AS PROPRIEDADES DA LISTA E DIMINUIR A LOGICA TALVEZ?
                if(value.GetType() != propTarget.PropertyType)
                {
                    if (propTarget.PropertyType == typeof(Enum))
                    {
                        Console.WriteLine(value.GetType() + "\n \n");
                        value = value.ChangeTypeToEnum(targetType);
                        Console.WriteLine(JsonManipulation.Serialize(value).Result);
                    }
                    else
                        value.FromTo();
                }
                propTarget.SetValue(target, value);
            }
            return target;
        }

        public static object ChangeListToListEnum(this object obj, Type type)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var list = new List<object>();

            if (obj.GetType().IsEnumerable())
            {
                foreach (var item in (IList) obj)
                    list.Add(ChangeTypeToEnum(obj, type.GetGenericArguments()[0]));
            }
            else if (type.IsEnumerable() == false)
                return ChangeTypeToEnum(obj, type);
            return list;
        }

        public static object ChangeTypeToEnum(this object obj, Type type)
        {
            bool typeIsEnum = type == typeof(Enum);
            if (obj.GetType().IsEnumerable() && type.IsEnumerable())
                return ChangeListToListEnum(obj, type);
            if (typeIsEnum && obj.GetType() == typeof(string))
            {
                if (Enum.TryParse(type, obj.ToString(), out var result))
                    return result;
                Console.WriteLine("Teste \n \n \n");
            }
            else if(typeIsEnum && obj.TryParseToInt32(out var value))
                return Enum.ToObject(type, value);
            throw new Exception("Object and type destine isn't compatible");
        }

        public static bool TryParseToInt32(this object obj, out int value)
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
