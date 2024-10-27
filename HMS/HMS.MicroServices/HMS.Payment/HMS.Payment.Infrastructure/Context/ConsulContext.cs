using Constructs;
using HashiCorp.Cdktf.Providers.Consul.DataConsulKeys;
using HashiCorp.Cdktf.Providers.Consul.Provider;

namespace HMS.Payments.Infrastructure.Context
{
    public sealed class ConsulContext : Construct
    {
        private ConsulProvider _provider;
        public ConsulContext(Construct scope, string id) : base(scope, id) 
        {
            _provider = new(this, "Consul", new ConsulProviderConfig
            {
                Address = "http://localhost:8500"
            });
        }
        public void AddKvKey(string data, string kvKey)
        {
            var kv = new ConsulKeyValue(this, "")
            {
                Key = "",
                Value 
            };
            
            
        }
    }
}
