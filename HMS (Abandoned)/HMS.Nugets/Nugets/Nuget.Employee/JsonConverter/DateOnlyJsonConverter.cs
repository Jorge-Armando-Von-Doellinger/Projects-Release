using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HMS.Employee.Application.JsonConverter
{
    internal class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Lê a string do JSON e converte para DateOnly usando o formato especificado
            var dateString = reader.GetString();
            if (DateOnly.TryParseExact(dateString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }
            throw new JsonException($"Formato de data inválido: {dateString}");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            // Serializa o DateOnly como string usando o formato especificado
            writer.WriteStringValue(value.ToString("dd/MM/yyyy"));
        }
    }
}
