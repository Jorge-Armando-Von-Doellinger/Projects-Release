using HMS.Payments.Application.Enums.Keys.Schemas;
using HMS.Payments.Application.Interfaces.Services;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Update;

namespace HMS.Payments.API.Services.Startup
{
    public sealed class RegisterSchemas : IHostedService
    {
        private readonly ISchemasService _schemaService;
        private ICacheService _cacheService;

        public RegisterSchemas(ISchemasService schemaService, ICacheService cacheService)
        {
            _schemaService = schemaService;
            _cacheService = cacheService;
            StartAsync(new());
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                SetSchema<PaymentModel>(SchemasDefaultKeysEnum.Payment_Add_Schema.ToString());
                SetSchema<PaymentUpdateModel>(SchemasDefaultKeysEnum.Payment_Update_Schema.ToString());
                SetSchema<PaymentEmployeeModel>(SchemasDefaultKeysEnum.PaymentEmployee_Add_Schema.ToString());
                SetSchema<PaymentEmployeeUpdateModel>(SchemasDefaultKeysEnum.PaymentEmployee_Update_Schema.ToString());
            });
        }

        private void SetSchema<T>(string key)
        {
            Console.WriteLine(key);
            var schema = _schemaService.FromType<T>();
            Console.WriteLine(schema.ToJson());
            _cacheService.Set(key, schema);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
