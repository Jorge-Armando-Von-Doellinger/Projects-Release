using System.Text.Json.Serialization;
using System.Text.Json;

public class GuidConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string guidString = reader.GetString();
        if(Guid.TryParse(guidString, out Guid guid))
        {
            return guid;
        }
        Console.WriteLine(guidString + "guid string \n \n \n");
        return Guid.Empty;  // Ou talvez lançar uma exceção, dependendo do seu caso de uso
    }

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
