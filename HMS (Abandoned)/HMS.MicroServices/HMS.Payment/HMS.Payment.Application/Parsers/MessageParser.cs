using System.Text;
using System.Text.Json;
using HMS.Payments.Application.Exceptions;
using HMS.Payments.Application.Services;

namespace HMS.Payments.Application.Parsers;

public sealed class MessageParser
{
    private readonly JsonSchemaService _jsonSchemaService;

    public MessageParser(JsonSchemaService jsonSchemaService)
    {
        _jsonSchemaService = jsonSchemaService;
    }

    internal async Task<TModel> Parse<TModel>(byte[] message) where TModel : new()
    {
        var json = Encoding.UTF8.GetString(message);
        var valid = await _jsonSchemaService.ValidateJson<TModel>(json);
        if (!valid) throw new InvalidMessageException("Invalid JSON");
        var model = JsonSerializer.Deserialize<TModel>(json) ?? throw new InvalidMessageException("Invalid JSON");
        return model;
    }
}