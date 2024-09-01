using HMS.Employee.Core.Entity;
using Nuget.Employee.Inputs;
using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace HMS.Employee.Application.Mappers
{
    public static class ObjectMapper
    {
        public async static Task<T> Mapper<T>(object obj) where T : new()
        {
            try
            {
                if (obj == null)
                    throw new ArgumentNullException(nameof(T), "Objeto nulo");
                return await Task.Run(() =>
                {
                    T objDestination = new T();
                    PropertyInfo[] destinationProperties = objDestination.GetType().GetProperties();

                    foreach (var prop in destinationProperties)
                    {
                        var sourceProp = obj.GetType().GetProperty(prop.Name);
                        if (sourceProp != null)
                        {
                            var value = sourceProp.GetValue(obj);
                            if(value != null)
                            {
                                object convertedValue = Convert.ChangeType(value, prop.PropertyType);
                                prop.SetValue(objDestination, convertedValue);
                            }       
                        }
                    }
                    return objDestination;
                });
            }
            catch (Exception ex)
            {
                throw; // Para desenvolviment §
            }
            
        }
    }
}
