using NJsonSchema;

namespace HMS.Payments.Application.Services;

public sealed class JsonSchemaService
{
    internal JsonSchema GetSchema<T>()
    {
        return JsonSchema.FromType<T>();
    }

    internal async Task<bool> ValidateJson<TEntity>(string json) where TEntity : new()
    {
        var schema = JsonSchema.FromType(typeof(TEntity));
        schema.AllowAdditionalProperties = false;
        var errors = schema.Validate(json);
        foreach (var error in errors)
            Console.WriteLine(error.Property);
        return errors.Count == 0;
    }
}