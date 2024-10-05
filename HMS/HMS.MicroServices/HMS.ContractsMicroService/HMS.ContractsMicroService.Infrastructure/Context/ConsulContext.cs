using Consul;
using HMS.ContractsMicroService.Core.Json;
using Newtonsoft.Json;
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

        private string GetDefaultKey(string appName) => $"hms/microservices/{appName}/settings";

        public async Task InsertOrUpdate(byte[] dataBytes, string appName)
        {
            try
            {
                var kvKey = GetDefaultKey(appName);
                var kvPair = new KVPair(kvKey) { Value = dataBytes };
                await _client.KV.Put(kvPair);
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

        public async Task DeleteAsync(string appName)
        {
            try
            {
                var kvKey = GetDefaultKey(appName);
                await _client.KV.Delete(kvKey);
                Console.WriteLine("-----------------------\n KvKey DELETADA COM SUCESSO! \n -------------------------------");
            }
            catch(Exception ex)
            {
                ErrorInOperation(ex.Message);
            }
        }

        public async Task<T> GetSettings<T>(string appName)
        {
            var kvKey = GetDefaultKey(appName);
            var data = await _client.KV.Get(kvKey);
            var json = Encoding.UTF8.GetString(data.Response?.Value);
            var obj = await JsonManipulation.Deserialize<T>(json);
            return obj;
        }
    }
}
