using HMS.Payments.Core.Entity.Base;
using HMS.Payments.Core.Enums;
using HMS.Payments.Core.Extensions;
using HMS.Payments.Core.Json;
using System.Collections;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;

namespace HMS.Payments.Core.Mapper
{
    public static class ObjectExtension
    {

        // Mapeia de um objeto para o outro
        // Mapping from one objet to another
/*        public static TTarget FromObjectTo<TTarget>(this Object source) where TTarget : new()
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
                    var targetProperty = targetProperties
                        .FirstOrDefault(x => x.Name == property.Name);
                    if (targetProperty == null) 
                        continue;
                    var value = property.GetValue(source);
                    if (value == null)
                        continue;
                    if (value.GetType() != targetProperty.GetValue(target).GetType())
                        value = FromObjectTo(source, targetProperty.GetValue(target).GetType());
                    targetProperty.SetValue(target, value); 
                }
                return target;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static object FromObjectTo(this Object source, Type type)
        {
            //Verificar se ambos sao listas
            try
            {
                // Ainda com problemas em mapear listas
                if (source == null)
                    throw new ArgumentNullException(nameof(source), "Source cannot be null");
                if (source.GetType().IsTypeEnumerable() == true)
                    throw new TypeAccessException("Não é possivel passar os dados de uma lista para outra. Use a extensão propria para listas!");

                var target = Expression.New(type);
                var targetProperties = target.GetType().GetProperties();
                
                foreach (var property in source.GetType().GetProperties())
                {
                    var targetProperty = targetProperties
                        .FirstOrDefault(x => x.Name == property.Name);
                    if (targetProperty == null)
                        continue;
                    var value = property.GetValue(source);
                    if (value == null)
                        continue;
                    targetProperty.SetValue(target, value);
                }
                return target;
            }
            catch (Exception ex)
            {
                throw;
            }
        }*/

        // Mapeia de dois objetos iguais para o com mais informação, ou vice-versa
        public static Obj Replacer<Obj, EntityBase>(this Object obj, Obj valuesToReplace)
        {
            try
            {
                if (obj.GetType() != valuesToReplace.GetType())
                    throw new TypeLoadException("Objects in diferents types");
                var replace = valuesToReplace.GetType().GetProperties();
                var objProperties = obj.GetType().GetProperties();
                foreach (var property in replace)
                {
                    var valueOnBase = typeof(EntityBase).GetProperty(property.Name) != null;
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

        public static TTarget FromObjectTo<TTarget>(this object source) where TTarget : new()
        {
            if (source == null) throw new Exception("Source cannot be null");
            if (source.GetType().IsTypeEnumerable() == true && !typeof(TTarget).IsAssignableFrom(typeof(IEnumerable))) throw new TypeAccessException("Não é possível passar os dados de uma lista para um objeto. Use a extensão própria para listas!");

            if (source.GetType().IsTypeEnumerable() == true)
            {
                var sourceList = (IEnumerable) source;
                var targetListType = typeof(TTarget);
                var itemType = targetListType.GetGenericArguments().First();
                var targetList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));

                foreach (var item in sourceList)
                {
                    var targetItem = Activator.CreateInstance(itemType);
                    MapProperties(item, targetItem);
                    targetList.Add(targetItem);
                }

                return (TTarget)(object)targetList;
            }
            else
            {
                Console.WriteLine(typeof(TTarget));
                var target = new TTarget();
                MapProperties(source, target);
                return target;
            }
        }

        private static void MapProperties(object source, object target)
        {
            if (source == null || target == null)
                throw new NullReferenceException("Source or Target cannot be null");

            var sourceProperties = source.GetType().GetProperties();
            var targetProperties = target.GetType().GetProperties();

            foreach (var sourceProp in sourceProperties)
            {
                // Ignorar propriedades indexadas
                if (sourceProp.GetIndexParameters().Length > 0)
                    continue;

                var targetProp = targetProperties.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (targetProp == null || !targetProp.CanWrite)
                    continue; // Ignora se não for possível escrever na propriedade
                
                var value = sourceProp.GetValue(source);
                if (value == null)
                    continue;

                if (targetProp.PropertyType.IsClass && targetProp.PropertyType != typeof(string))
                {
                    var targetComplexObject = Activator.CreateInstance(targetProp.PropertyType);
                    if (value.GetType() == typeof(List<string>) && targetProp.PropertyType.IsEnumEnumerable() == true)
                    {
                        var targetValues = targetProp.GetValue(target);
                        if (targetProp.PropertyType == typeof(List<BenefitsEnum>))
                            targetComplexObject = ((List<string>)value).ToListEnum<BenefitsEnum>((List<BenefitsEnum>) targetValues);
                        if (targetProp.PropertyType == typeof(List<MandatoryDiscountsEnum>))
                            targetComplexObject = ((List<string>)value).ToListEnum<MandatoryDiscountsEnum>((List<MandatoryDiscountsEnum>) targetValues);
                        targetProp.SetValue(target, targetComplexObject);
                        continue;
                    }
                    else if(value.GetType() == targetProp.PropertyType && value.GetType() == typeof(List<string>))
                    {
                        targetProp.SetValue(target, value);
                        continue;
                    }
                    MapProperties(value, targetComplexObject);
                    targetProp.SetValue(target, targetComplexObject);
                }
                else if (targetProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                {
                    targetProp.SetValue(target, value);
                }
                else
                {
                    try
                    {
                        var convertedValue = Convert.ChangeType(value, targetProp.PropertyType);
                        targetProp.SetValue(target, convertedValue);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
    }
}
