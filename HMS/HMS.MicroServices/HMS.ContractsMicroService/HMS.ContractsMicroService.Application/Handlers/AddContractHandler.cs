﻿using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Interfaces.Messaging;

namespace HMS.ContractsMicroService.Application.Handlers
{
    internal sealed class AddContractHandler : IMessageHandler
    {
        private readonly IContractManager _manager;
        private readonly IEmployeeContractManager _employeeManager;
        private readonly IWorkHoursManager _workHoursManager;
        public AddContractHandler(IContractManager manager)
        {
            _manager = manager;
            DtoType = typeof();
        }

        public Type DtoType{ get; private set; }

        public async Task HandleAsync(object input)
        {
            if (input.GetType().Equals(typeof()))  await _manager.Add((ContractInput)
                input);
            else if (input.GetType().Equals(typeof()))  await _employeeManager.Add((EmployeeContractInput)input);
            else throw new InvalidDataException("Input for add isn't compatible");
        }
    }
}
