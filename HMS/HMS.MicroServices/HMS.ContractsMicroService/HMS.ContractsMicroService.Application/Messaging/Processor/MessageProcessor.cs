using HMS.ContractsMicroService.Application.Enums;
using HMS.ContractsMicroService.Application.Exceptions;
using HMS.ContractsMicroService.Application.Handlers;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Data;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace HMS.ContractsMicroService.Application.Messaging.Processor
{
    public sealed class MessageProcessor : IMessageProcessor
    {
        private Dictionary<string, IMessageHandler> _EmployeeHandler;
        
        private readonly IEmployeeContractManager _employeeContract;
        private readonly IContractManager _contractManager;
        public MessageProcessor(IServiceProvider provider)
        {
            var service = provider.CreateScope().ServiceProvider;
            _contractManager = service.GetRequiredService<IContractManager>();
            _employeeContract = service.GetRequiredService<IEmployeeContractManager>();
        }
        public async Task Process(MessagingData data)
        {
            try
            {
                if (_EmployeeHandler.TryGetValue(data.CurrentKey, out var handler) == false)
                    throw new InvalidDataException("CurrentKey inválida");
                var obj = JsonSerializer.Deserialize(data.Json, handler.DtoType);
                await handler.HandleAsync(obj);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar mensagem!");
                Console.WriteLine($" --> Mensagem: {ex.Message}");
                throw;
            }
        }

        private void SetHandlers()
        {
            SetEmployeeContractHandler();
            SetGeneralContractHandler();
        }

        private void SetGeneralContractHandler()
        {
            var errorInGetSettings = generalSettingsSuccess;
            var settingsIsNull = generalSettings == null;
            SettingsIsValid(errorInGetSettings || settingsIsNull);
            _EmployeeHandler.Add(generalSettings.AddKey, new AddContractHandler(_contractManager));
            _EmployeeHandler.Add(generalSettings.UpdateKey, new UpdateContractHandler(_contractManager));
            _EmployeeHandler.Add(generalSettings.DeleteKey, new DeleteContractHandler(_contractManager));
        }

        private void SetEmployeeContractHandler()
        {
            var employeeContractSettingsSuccess = _messagingSystem.TryGetValue(MessagingSystemKeysEnum.EmployeeContracts.ToString(), out var employeeSettings);
            bool settingsIsntNull = employeeSettings != null;
            SettingsIsValid(employeeContractSettingsSuccess && settingsIsntNull);
            _EmployeeHandler.Add(employeeSettings.AddKey, new AddEmployeeHandler(_employeeContract));
            _EmployeeHandler.Add(employeeSettings.UpdateKey, new UpdateEmployeeHandler(_employeeContract));
            _EmployeeHandler.Add(employeeSettings.DeleteKey, new DeleteEmployeeHandler(_employeeContract));
        }

        private void SettingsIsValid(bool valid)
        {
            if (valid) return;
            else throw new InvalidSettingsException("Erro ao processar esta rota de contratos!");
        }
    }
}
