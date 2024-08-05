using HMS.Core.Entity;

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
            return new OutputModel();
        }

        internal List<OutputModel> Map(List<ClientEntity> clients)
        {
            List<OutputModel> outputs = new List<OutputModel>();
            foreach(var client in clients)
            {

            }
            return outputs;
        }
    }
}
