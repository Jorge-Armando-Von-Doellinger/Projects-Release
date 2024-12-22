using HMS.Employee.Core.Entity;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Nuget.Employee.Data.Discounts;
using Nuget.Employee.Inputs;
using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace HMS.Employee.Application.Mappers
{
    internal static class ObjectMapper
    {
        internal async static Task<T> Mapper<T>(object obj) where T : new()
        {
            try
            {
                if (obj == null)
                    throw new ArgumentNullException(nameof(T), "Objeto nulo");

                return await Task.Run(async () =>
                {
                    T objDestination = new T();
                    PropertyInfo[] destinationProperties = objDestination.GetType().GetProperties();

                    foreach (var prop in destinationProperties)
                    {
                        var sourceProp = obj.GetType().GetProperty(prop.Name);
                        if (sourceProp != null)
                        {
                            var value = sourceProp.GetValue(obj);
                            if (value != null)
                            {

                                object convertedValue = null;
                                try
                                {
                                    if (value.GetType() == typeof(Discount))
                                    {
                                        Console.WriteLine();
                                        convertedValue = await ToTargetType<Discount, Core.Data.Discounts.Discount>((Discount)value); // Deu erro aqui
                                    }
                                    else if (prop.PropertyType.IsEnum)
                                    {
                                        convertedValue = Enum.ToObject(prop.PropertyType, value);
                                    }
                                    else
                                    {
                                        convertedValue = Convert.ChangeType(value, prop.PropertyType);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw;
                                }

                                prop.SetValue(objDestination, convertedValue);
                            }
                        }
                    }
                    return objDestination;
                });
            }
            catch (Exception ex)
            {
                throw; // Para desenvolvimento
            }

        }

        internal async static Task<TTarget> ToTargetType<TSource, TTarget>(TSource source) where TTarget : new () where TSource : class
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var target = new TTarget();

            var sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProperties = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProperties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name == sourceProp.Name);
                if (targetProp != null && targetProp.CanWrite)
                {
                    var value = sourceProp.GetValue(source);
                    if (value != null)
                    {
                        targetProp.SetValue(target, value);
                    }
                }
            }
            return target;
        }
    }
}
