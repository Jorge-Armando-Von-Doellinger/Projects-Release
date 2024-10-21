using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.DTOs.UpdateInput;
using HMS.ContractsMicroService.Core.Interfaces.Services;
using NJsonSchema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HMS.ContractsMicroService.API.Services.Hosted
{
    public class RegisterDtosSchemas : IHostedService
    {
        private readonly ICacheService _cache;
        private readonly ISettingsService _settingsService;
        public RegisterDtosSchemas(ICacheService cache, ISettingsService settingsService)
        {
            _cache = cache;
            _settingsService = settingsService;
        }

        private async Task SetSchemas()
        {
            await SetContractSchema();
            await SetEmployeeContractSchema();
        }

        private async Task SetSchema<T>() where T : new()
        {
            var schema = JsonSchema.FromType<T>();
            string name = typeof(T).Name;
            await _settingsService.RegisterSchemas(typeof(T), name);
            _cache.Set(name, schema);
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
