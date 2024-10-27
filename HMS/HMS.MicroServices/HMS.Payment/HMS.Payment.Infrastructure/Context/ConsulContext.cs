using Constructs;
using HashiCorp.Cdktf.Providers.Consul.Provider;

namespace HMS.Payments.Infrastructure.Context
{
    public sealed class ConsulContext : Construct
    {
        private ConsulProvider _provider;
        public ConsulContext(Construct scope, string id) : base(scope, id) 
        {
            
        }

        internal void ConfigureProvider()
        {
            _provider = new()
            {
                
            }
        }
        
    }
}
