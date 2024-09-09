using HMS.Employee.Core.Entity.Base;
using HMS.Employee.Core.Extensions;
using HMS.Employee.Core.Json;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace HMS.Employee.Core.Mapper
{
    public static class ObjectExtension
    {

        // Mapeia de um objeto para o outro
        // Mapping from one objet to another
        public static TTarget FromObjectTo<TTarget>(this Object source) where TTarget : new()
        {
            //Verificar se ambos sao listas
            try
            {
                // Ainda com problemas em mapear listas
                if (source == null)
                    throw new ArgumentNullException(nameof(source), "Source cannot be null");
                if (source.GetType().IsTypeEnumerable() == true)
                    throw new TypeAccessException("Não é possivel passar os dados de uma lista para outra. Use a extensão propria para listas!");

                var target = new TTarget();
                var targetProperties = target.GetType().GetProperties();

                foreach (var property in source.GetType().GetProperties())
                {
                    var targetProperty = targetProperties.FirstOrDefault(x => x.Name == property.Name);
                    if (targetProperty == null)
                        continue;
                    var value = property.GetValue(source);
                    if (value == null)
                        continue;
                    targetProperty.SetValue(target, value); // Deu erro aqui
                }
                return target;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Mapeia de dois objetos iguais para o com mais informação, ou vice-versa
        public static Obj Replacer<Obj>(this Object obj, Obj valuesToReplace)
        {
            try
            {
                if (obj.GetType() != valuesToReplace.GetType())
                    throw new TypeLoadException("Objects in diferents types");
                var replace = valuesToReplace.GetType().GetProperties();
                var objProperties = obj.GetType().GetProperties();
                foreach (var property in replace)
                {
                    var valueOnBase = typeof(BaseEntityWithEmployee).GetProperty(property.Name) != null;
                    if (valueOnBase == false)
                    {
                        var objProperty = objProperties.FirstOrDefault(x => x.Name == property.Name);
                        if (objProperty != null && objProperty.CanWrite)
                        {
                            var value = property.GetValue(valuesToReplace);
                            bool? IsDefaultValue = null;
                            if (value == null)
                                continue;
                            var type = value.GetType();
                            if (type.IsValueType)
                            {
                                IsDefaultValue = value.Equals(Activator.CreateInstance(type));
                            }
                            else if (type.IsEnum)
                            {
                                IsDefaultValue = Convert.ToInt32(value) == 0;
                            }
                            if (value != null && IsDefaultValue == false)
                            {
                                objProperty.SetValue(obj, value);
                            }
                        }

                    }
                }
                return (Obj)obj;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
