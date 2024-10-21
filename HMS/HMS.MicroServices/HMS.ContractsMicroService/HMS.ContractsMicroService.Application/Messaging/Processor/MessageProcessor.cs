using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.DTOs.UpdateInput;
using HMS.ContractsMicroService.Application.Handlers;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Data;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using System.Text.Json;

namespace HMS.ContractsMicroService.Application.Messaging.Processor
{
    public sealed class MessageProcessor : IMessageProcessor
    {
        private readonly Dictionary<JsonSchema, IMessageHandler> _EmployeeHandler = new();
        private readonly ICacheService _cache;

        private readonly IEmployeeContractManager _employeeContract;
        private readonly IContractManager _contractManager;
        public MessageProcessor(IServiceProvider provider, ICacheService cache)
        {
            var service = provider.CreateScope().ServiceProvider;
            _contractManager = service.GetRequiredService<IContractManager>();
            _employeeContract = service.GetRequiredService<IEmployeeContractManager>();
            _cache = cache;
            SetHandlers();
        }
        public async Task Process(string data)
        {
            try
            {
                var handler = FindHandlerByJson(data) ?? throw new Exception("Teste");
                var obj = JsonSerializer.Deserialize(data, handler.DtoType);
                await handler.HandleAsync(obj);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar mensagem!");
                Console.WriteLine($" --> Mensagem: {ex.Message}");
                throw;
            }
        }

        private IMessageHandler FindHandlerByJson(string json)
        {
            var handler = _EmployeeHandler.FirstOrDefault(pair =>
            {
                var errors = pair.Key.Validate(json);
                Console.WriteLine(errors.Count);
                return errors.Count == 0;
            });
            if(handler.Value.DtoType != null) Console.WriteLine(handler.Value.DtoType);
            if(handler.Value.DtoType == null) Console.WriteLine(handler.Value.DtoType);
            
            return handler.Value;
        }

        private void SetHandlers()
        {
            SetEmployeeContractHandler();
            SetGeneralContractHandler();
        }

        private void SetGeneralContractHandler()
        {
            var inputSchema = _cache.Get<JsonSchema>(nameof(ContractInput)) ?? throw new Exception("Teste");
            var updateInputSchema = _cache.Get<JsonSchema>(nameof(ContractUpdateInput));
            var deleteInputSchema = _cache.Get<JsonSchema>(nameof(ContractDeleteInput));
            _EmployeeHandler.Add(inputSchema, new AddContractHandler(_contractManager));
            _EmployeeHandler.Add(updateInputSchema, new UpdateContractHandler(_contractManager));
            _EmployeeHandler.Add(deleteInputSchema, new DeleteContractHandler(_contractManager));
        }

        private void SetEmployeeContractHandler()
        {
            var inputSchema = _cache.Get<JsonSchema>(nameof(EmployeeContractInput));
            var updateInputSchema = _cache.Get<JsonSchema>(nameof(EmployeeContractUpdateInput));
            var deleteInputSchema = _cache.Get<JsonSchema>(nameof(EmployeeContractDeleteInput));
            _EmployeeHandler.Add(inputSchema, new AddEmployeeHandler(_employeeContract));
            _EmployeeHandler.Add(updateInputSchema, new UpdateEmployeeHandler(_employeeContract));
            _EmployeeHandler.Add(deleteInputSchema, new DeleteEmployeeHandler(_employeeContract));
        }
    }
}
