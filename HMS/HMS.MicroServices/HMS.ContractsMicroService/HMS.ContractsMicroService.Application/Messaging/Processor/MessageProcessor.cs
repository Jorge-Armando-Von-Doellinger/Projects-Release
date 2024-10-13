using HMS.ContractsMicroService.Application.Handlers;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Data;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Settings;
using System.Text.Json;

namespace HMS.ContractsMicroService.Application.Messaging.Processor
{
    public sealed class MessageProcessor : IMessageProcessor
    {
        private readonly Dictionary<string, IMessageHandler> _EmployeeHandler;
        private IEmployeeContractManager _contractManager;
        public MessageProcessor(IServiceProvider provider)
        {
            var service = provider.CreateScope().ServiceProvider.GetRequiredService<IEmployeeContractManager>();
            _contractManager = service; 
            var settings = AppSettings.CurrentSettings.RabbitMq;

            _EmployeeHandler = new()
            {
                { settings.AddKey, new AddEmployeeHandler(_contractManager) },
                { settings.UpdateKey, new UpdateEmployeeHandler(_contractManager) },
                { settings.DeleteKey, new DeleteEmployeeHandler(_contractManager) },
                { settings. }
            };
            
        }
        public async Task Process(MessagingData data)
        {
            try
            {
                if(_EmployeeHandler.TryGetValue(data.CurrentKey, out var handler) == false)
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
    }
}
