using HMS.Payments.Application.Enums.Keys.Schemas;
using HMS.Payments.Application.Handlers.Message;
using HMS.Payments.Application.Interfaces.Handlers;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Interfaces.Services;
using NJsonSchema;

namespace HMS.Payments.Application.Services;

public sealed class HandlerService
{
    private readonly ISchemasModelService _schemasModelService;
    private readonly IPaymentManager _paymentManager;
    private readonly IPaymentEmployeeManager _paymentEmployeeManager;
    private Dictionary<JsonSchema, IMessageHandler> _handler = new();

    public HandlerService(ISchemasModelService schemasModelService, 
        IPaymentManager paymentManager,
        IPaymentEmployeeManager paymentEmployeeManager)
    {
        _schemasModelService = schemasModelService;
        _paymentManager = paymentManager;
        _paymentEmployeeManager = paymentEmployeeManager;
        AddHandlers();
    }

    internal IMessageHandler? GetHandler(string json)
    {
        return _handler.FirstOrDefault(x => 
            x.Key.Validate(json).Count == 0)
            .Value;
    }
    private void AddHandlers()
    {
        AddPaymentHandler();
        AddPaymentEmployeeHandler();
    }
    private void AddPaymentHandler()
    {
        _handler.Add(GetSchema(SchemasDefaultKeysEnum.Payment_Add_Schema), new PaymentAddHandler(_paymentManager));
        _handler.Add(GetSchema(SchemasDefaultKeysEnum.Payment_Update_Schema), new PaymentUpdateHandler(_paymentManager));
    }
    private void AddPaymentEmployeeHandler()
    {
        _handler.Add(GetSchema(SchemasDefaultKeysEnum.PaymentEmployee_Add_Schema), new PaymentEmployeeAddHandler(_paymentEmployeeManager));
        _handler.Add(GetSchema(SchemasDefaultKeysEnum.PaymentEmployee_Update_Schema), new PaymentEmplyoeeUpdateHandler(_paymentEmployeeManager));
    }
    private JsonSchema GetSchema(SchemasDefaultKeysEnum key)
    {
        var data = _schemasModelService.GetDtosSchemas().FirstOrDefault(x => x.Key == key).Value;
        return data;
    }
}