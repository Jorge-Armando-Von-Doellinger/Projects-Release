using HMS.Payments.Application.Enums.Keys.Schemas;
using HMS.Payments.Application.Interfaces.Services;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Update;
using NJsonSchema;

namespace HMS.Payments.Application.Services
{
    public sealed class SchemasModelService : ISchemasModelService
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

        public Dictionary<SchemasDefaultKeysEnum, JsonSchema> GetDtosSchemas()
        {
            Console.WriteLine(FromType<PaymentEmployeeModel>().ToJson());
            Dictionary<SchemasDefaultKeysEnum, JsonSchema> schemas = new();
            schemas.Add(SchemasDefaultKeysEnum.Payment_Add_Schema, FromType<PaymentModel>());
            schemas.Add(SchemasDefaultKeysEnum.Payment_Update_Schema, FromType<PaymentUpdateModel>());
            schemas.Add(SchemasDefaultKeysEnum.PaymentEmployee_Add_Schema, FromType<PaymentEmployeeModel>());
            schemas.Add(SchemasDefaultKeysEnum.PaymentEmployee_Update_Schema, FromType<PaymentEmployeeUpdateModel>());
            return schemas;
        }
    }
}
