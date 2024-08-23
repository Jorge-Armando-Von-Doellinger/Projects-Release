using Gateway.v1.Application.Enums;
using Gateway.v1.Application.Interfaces;
using Gateway.v1.Application.Managers;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Gateway.v1.Application.Factories
{
    public sealed class ManagerFactory
    {
        private IServiceProvider _serviceProvider;
        public ManagerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            GetManager(ManageFactoryEnums.ClientManager);
        }

        public IManager GetManager(Enum num)
        {
            
            IManager manager = default;
            switch (num)
            {
                case ManageFactoryEnums.ClientManager:
                    Console.WriteLine("\n \n \n Teste1 \n \n \n");
                    manager = _serviceProvider.GetRequiredService<ClientManager>();
                    break;
                default:
                    throw new Exception("Enum incorreto! ManagerFactory-GetManager!");
            }
            if (num == null)
                Console.WriteLine("\n \n \n Null \n \n \n");
            return manager;
        }
    }
}
