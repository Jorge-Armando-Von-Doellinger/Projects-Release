using NJsonSchema;

namespace HMS.Payments.Application.Interfaces.Services;

public interface ISchemasService
{
    JsonSchema FromType(Type t);
    JsonSchema FromType<T>();
    JsonSchema FromJson(string json);
}