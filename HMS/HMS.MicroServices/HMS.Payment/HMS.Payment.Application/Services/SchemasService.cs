using HMS.Payments.Application.Interfaces.Services;
using NJsonSchema;

namespace HMS.Payments.Application.Services
{
    public sealed class SchemasService : ISchemasService
    {
        public JsonSchema FromJson(string json)
        {
            return JsonSchema.FromSampleJson(json);
        }

        public JsonSchema FromType(Type type)
        {
            return JsonSchema.FromType(type);
        }

        public JsonSchema FromType<T>()
        {
            return JsonSchema.FromType<T>();
        }
    }
}
