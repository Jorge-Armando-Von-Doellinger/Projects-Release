using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Core.Data;
using HMS.ContractsMicroService.Core.Extensions;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Json;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Contracts.Inputs;
using Nuget.MessagingUtilities;
using Nuget.Settings;
using Nuget.Settings.Messaging;
using System;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HMS.ContractsMicroService.Application.Messaging.Processor
{
    public sealed class MessageProcessor : IMessageProcessor
    {
        private readonly Dictionary<string, Func<object, Task>> _EmployeeHandler = new ();
        private IEmployeeContractManager _contractManager;
        public MessageProcessor(IServiceProvider provider)
        {
            var service = provider.CreateScope().ServiceProvider.GetRequiredService<IEmployeeContractManager>();
            _contractManager = service;

            var settings = AppSettings.CurrentSettings.RabbitMq;

            var addFunc = async (object input) => await _contractManager.Add((EmployeeContractInput)input);
            var deleteFunc = async (object input) => await _contractManager.Delete((string)input);
            var updateFunc = async (object input) => await _contractManager.Update((EmployeeContractUpdateInput)input);
            _EmployeeHandler.Add(settings.AddKey, addFunc);
            _EmployeeHandler.Add(settings.DeleteKey, deleteFunc);
            _EmployeeHandler.Add(settings.UpdateKey, updateFunc);
        }
        private async Task<T> Desserialize<T>(string json)
        {
            return await JsonManipulation.Deserialize<T>(json);
        }
        public async Task Process(MessagingData data)
        {
            Console.WriteLine(data.CurrentKey);
            var task = _EmployeeHandler.TryGetValue(data.CurrentKey, out var func)
                ? true : throw new InvalidDataException("CurrentKey Not Founded");
            _ = _EmployeeHandler.TryGetValue(data.CurrentKey, out var method) ? method : throw new Exception();
            var obj = await JsonManipulation.Deserialize(data.Json, GetDtoType(data.CurrentKey));
            await method(obj);
        }

        private Type GetDtoType(string key)
        {
            var settings = AppSettings.CurrentSettings.RabbitMq;
            var types = new Dictionary<string, Type>();
            types.Add(settings.AddKey, typeof(EmployeeContractInput));
            types.Add(settings.UpdateKey, typeof(EmployeeContractUpdateInput));
            types.Add(settings.DeleteKey, typeof(string));
            if (types.TryGetValue(key, out var type))
                return type;
            else
                return null;
        }
    }
}
