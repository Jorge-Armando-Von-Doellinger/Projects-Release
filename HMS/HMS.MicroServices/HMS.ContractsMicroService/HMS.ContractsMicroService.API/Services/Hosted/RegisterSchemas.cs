using HMS.ContractsMicroService.API.Settings;
using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.DTOs.UpdateInput;
using HMS.ContractsMicroService.Core.Interfaces.Services;
using NJsonSchema;

namespace HMS.ContractsMicroService.API.Services.Hosted
{
    public class RegisterSchemas : IHostedService
    {
        private readonly ICacheService _cache;
        private readonly ISettingsService _settingsService;
        private readonly AppSettingsService _appSettingsService;
        private readonly IConfiguration _configuration;

        public RegisterSchemas(ICacheService cache, ISettingsService settingsService, AppSettingsService appSettingsService, IConfiguration configuration)
        {
            _cache = cache;
            _settingsService = settingsService;
            _appSettingsService = appSettingsService;
            _configuration = configuration;
        }

        private async Task SetSchemas()
        {
            await SetContractSchema();
            await SetEmployeeContractSchema();
            await SetAppSttingsSchema();
        }

        private async Task SetSchema<T>() where T : new()
        {
            var schema = JsonSchema.FromType<T>();
            string name = typeof(T).Name;
            await _settingsService.RegisterSchemas(typeof(T), name);
            _cache.Set(name, schema);
        }
        private async Task SetSchemaByJson(string json, string name)
        {
            var schema = await JsonSchema.FromJsonAsync(json);

            await _settingsService.RegisterSchemas(json, name);
            _cache.Set(name, schema);
        }

        internal async Task SetAppSttingsSchema() 
        {
            var json = _appSettingsService.GetDefaultSettings(_configuration);
            await SetSchemaByJson(json.GetRawText(), Keys.AppSettingsJson);
        }

        internal async Task SetContractSchema()
        {   
            await SetSchema<ContractInput>();
            await SetSchema<ContractUpdateInput>();
            await SetSchema<ContractDeleteInput>();
        }

        internal async Task SetEmployeeContractSchema()
        {
            await SetSchema<EmployeeContractInput>();
            await SetSchema<EmployeeContractUpdateInput>();
            await SetSchema<EmployeeContractDeleteInput>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await SetSchemas();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cache.RemoveAll();
            return Task.CompletedTask;
        }
    }
}
