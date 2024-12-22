using HMS.Core.Entity;
using Nuget.Client.Input;
using Nuget.Client.Output;

namespace HMS.Application.Mapper
{
    public sealed class ClientMapper
    {
        internal ClientEntity Map(InputModel input)
        {
            return new ClientEntity
            {
                Name = input.Name,
                CPF = input.CPF,
                DateBirth = input.DateBirth,
                EmailAdress = input.EmailAdress,
                PhoneNumber = input.PhoneNumber,
                YearsOld = input.YearsOld,
                RG = input.RG
            };
        }

        internal List<ClientEntity> Map(List<InputModel> inputs)
        {
            List<ClientEntity> clients = new List<ClientEntity>();
            foreach(var input in inputs)
            {
                clients.Add(Map(input));
            }
            return clients;
        }

        internal OutputModel Map(ClientEntity client)
        {
            return new OutputModel
            {
                Name = client.Name,
                CPF = client.CPF,
                RG = client.RG,
                EmailAdress = client.EmailAdress,
                PhoneNumber= client.PhoneNumber,
                DateBirth = client.DateBirth,
                YearsOld = client.YearsOld
            };
        }

        internal Task<List<OutputModel>> Map(List<ClientEntity> clients)
        {
            List<OutputModel> outputs = new List<OutputModel>();
            foreach(var client in clients)
            {
                outputs.Add(Map(client));
            }
            return Task.FromResult(outputs);
        }
        
        internal ClientEntity Map(UpdateModel updateModel)
        {
            var a =new ClientEntity
            {
                CPF = updateModel.CPF,
                DateBirth = updateModel.DateBirth,
                EmailAdress = updateModel.EmailAdress,
                Id = updateModel.Id,
                Name = updateModel.Name,
                PhoneNumber = updateModel.PhoneNumber,
                RG = updateModel.RG,
                YearsOld = updateModel.YearsOld
            };
            return a;
        }
    }
}
