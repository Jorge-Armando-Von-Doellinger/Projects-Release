using HMS.Employee.Application.Response;
using HMS.Employee.Core.Json;
using HMS.Employee.Infrastructure.MessageResponse;
using Nuget.Employee.Inputs;
using Nuget.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Employee.Application.Validator
{
    public class BasicValidator
    {
        public static async Task<Nuget.Response.Response> Validate<Input>(Input input)
        {
            if (input == null) 
                    return default;
            return await Task.Run(async () =>
            {
                PropertyInfo[] properties = input.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                List<string>? nullProperties = properties
                .Where(prop =>
                {
                    // Verifica se é valor padrao ou nulo.
                    var value = prop.GetValue(input);
                    var tipo = prop.PropertyType;
                    if (tipo.IsValueType)
                    {
                        var valorPadrao = Activator.CreateInstance(tipo);
                        return Equals(value, valorPadrao);
                    }
                    else if (tipo == typeof(string))
                    {
                        return string.IsNullOrEmpty((string)value);
                    }
                    return false;
                })
            .Select(prop =>
            {
                // Pega as propriedades que são nulas ou padrões
                return prop.Name + " Invalido!";
            })
            .ToList();
                return nullProperties.Count == 0 ? null
                    : await ResponseUseCase.GetResponseError(DefaultMessages.InvalidData, nullProperties);
            });
        }

        public static async Task<bool> IsDefaultValue<Value>(Value value)
        {
            var type = value.GetType();
            if(type.IsValueType) 
                return value.Equals(Activator.CreateInstance(type));
            return value == null;
        }
    }
}
