using HMS.Payments.Application.Enums.Keys.Schemas;
using HMS.Payments.Application.Handlers.Message;
using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.Services;
using HMS.Payments.Application.Interfaces.UseCases;
using NJsonSchema;

namespace HMS.Payments.Application.Services;

public sealed class HandlerService
{
    private readonly Dictionary<JsonSchema, IMessageHandler> _handler = new();
    private readonly IPaymentEmployeeUseCase _iPaymentEmployeeUseCase;
    private readonly IPaymentUseCase _iPaymentUseCase;
    private readonly ISchemasModelService _schemasModelService;

    public HandlerService(ISchemasModelService schemasModelService,
        IPaymentUseCase iPaymentUseCase,
        IPaymentEmployeeUseCase iPaymentEmployeeUseCase)
    {
        _schemasModelService = schemasModelService;
        _iPaymentUseCase = iPaymentUseCase;
        _iPaymentEmployeeUseCase = iPaymentEmployeeUseCase;
        AddHandlers();
    }

    internal async Task<IMessageHandler?> GetHandler(string json)
    {
        var handler = _handler
            .FirstOrDefault(x => x.Key.Validate(json).Count == 0)
            .Value;
        return handler;
    }

    private void AddHandlers()
    {
        AddPaymentHandler();
        AddPaymentEmployeeHandler();
    }

    private void AddPaymentHandler()
    {
        _handler.Add(GetSchema(SchemasDefaultKeysEnum.Payment_Add_Schema), new PaymentAddHandler(_iPaymentUseCase));
    }

    private void AddPaymentEmployeeHandler()
    {
        _handler.Add(GetSchema(SchemasDefaultKeysEnum.PaymentEmployee_Add_Schema),
            new PaymentEmployeeAddHandler(_iPaymentEmployeeUseCase));
    }

    private JsonSchema GetSchema(SchemasDefaultKeysEnum key)
    {
        var data = _schemasModelService.GetDtosSchemas().FirstOrDefault(x => x.Key == key).Value;
        return data;
    }
}