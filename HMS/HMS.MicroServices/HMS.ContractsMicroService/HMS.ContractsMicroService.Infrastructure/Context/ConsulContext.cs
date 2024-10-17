using Consul;
using HMS.ContractsMicroService.Core.Json;
using System.Text;

namespace HMS.ContractsMicroService.Infrastructure.Context
{
    public sealed class ConsulContext
    {
        private readonly ConsulClient _client;
        public ConsulContext()
        {
            _client = new ConsulClient();
        }

        private string GetDefaultKey() => $"hms/microservices/{appName}/settings";

        public async Task InsertOrUpdate(byte[] dataBytes)
        {
            try
            {
                var kvKey = GetDefaultKey();
                var kvPair = new KVPair(kvKey) { Value = dataBytes };
                var putSuccess = await _client.KV.Put(kvPair);
                if (!putSuccess.Response)
                {
                    Console.WriteLine("Falha ao inserir ou atualizar o par chave-valor no Consul.");
                }
                Console.WriteLine("-----------------------\n KvKey REGISTRADA COM SUCESSO! \n -------------------------------");
            }
            catch (Exception ex)
            {
                ErrorInOperation(ex.Message);
            }
        }

        private void ErrorInOperation(string message)
        {
            Console.WriteLine($"------------------- \n HOUVE UM ERRO NA OPERAÇÃO \n MESSAGE : {message} \n -------------------------");
            throw new InvalidOperationException(message);
        }

        public async Task DeleteAsync()
        {
            try
            {
                await _client.KV.Delete(GetDefaultKey());
                Console.WriteLine("-----------------------\n KvKey DELETADA COM SUCESSO! \n -------------------------------");
            }
            catch(Exception ex)
            {
                ErrorInOperation(ex.Message);
            }
        }

        public async Task<T> GetSettings<T>()
        {
            var kvKey = GetDefaultKey();
            var data = await _client.KV.Get(kvKey);
            var json = Encoding.UTF8.GetString(data.Response?.Value);
            var obj = await JsonManipulation.Deserialize<T>(json);
            return obj;
        }
    }
}
