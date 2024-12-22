using HMS.Payments.Application.Enums.Keys.Schemas;
using NJsonSchema;

namespace HMS.Payments.Application.Interfaces.Services;

public interface ISchemasModelService : ISchemasService
{
    internal Dictionary<SchemasDefaultKeysEnum, JsonSchema> GetDtosSchemas();
}