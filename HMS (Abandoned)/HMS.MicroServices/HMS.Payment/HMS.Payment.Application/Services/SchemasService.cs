using HMS.Payments.Application.Interfaces.Services;
using Newtonsoft.Json;
using NJsonSchema;

namespace HMS.Payments.Application.Services;

public class SchemasService : ISchemasService
{
    public JsonSchema FromJson(string json)
    {
        return JsonSchema.FromSampleJson(json);
    }

    public JsonSchema FromType(Type type)
    {
        var json = JsonConvert.SerializeObject(Activator.CreateInstance(type));
        json = json.ToLower();
        var schema = FromJson(json);
        schema.AllowAdditionalProperties = false;
        return schema;
    }

    public JsonSchema FromType<T>()
    {
        var schema = FromType(typeof(T));
        return schema;
    }
}