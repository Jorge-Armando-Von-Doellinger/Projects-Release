using Gateway.v1.Application.Enums;
using Gateway.v1.Application.Interfaces;
using Gateway.v1.Application.Managers;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Gateway.v1.Application.Factories
{
    public sealed class ManagerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ManagerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            GetManager(ManageFactoryEnums.ClientManager);
        }

        public IManager GetManager(Enum num)
        {
            IManager manager = null;
            switch (num)
            {
                case ManageFactoryEnums.ClientManager:
                    manager = _serviceProvider.GetRequiredService<ClientManager>();
                    break;
            }
            return manager;
        }
    }
}
