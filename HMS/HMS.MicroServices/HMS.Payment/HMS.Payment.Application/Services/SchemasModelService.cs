using HMS.Payments.Application.Enums.Keys.Schemas;
using HMS.Payments.Application.Interfaces.Services;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Update;
using NJsonSchema;

namespace HMS.Payments.Application.Services
{
    public sealed class SchemasModelService : SchemasService, ISchemasModelService
    {
        public Dictionary<SchemasDefaultKeysEnum, JsonSchema> GetDtosSchemas()
        {
            Dictionary<SchemasDefaultKeysEnum, JsonSchema> schemas = new()
            {
                { SchemasDefaultKeysEnum.Payment_Add_Schema, base.FromType<PaymentModel>() },
                { SchemasDefaultKeysEnum.Payment_Update_Schema, base.FromType<PaymentUpdateModel>() },
                { SchemasDefaultKeysEnum.PaymentEmployee_Add_Schema, base.FromType <PaymentEmployeeModel>() },
                { SchemasDefaultKeysEnum.PaymentEmployee_Update_Schema, base.FromType <PaymentEmployeeUpdateModel>() }
            };
            return schemas;
        }
    }
}
