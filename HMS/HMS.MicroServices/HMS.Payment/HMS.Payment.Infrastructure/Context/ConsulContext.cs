using Consul;

namespace HMS.Payments.Infrastructure.Context
{
    public sealed class ConsulContext
    {
        private readonly IConsulClient _client;
        public ConsulContext(IConsulClient client)
        {
            _client = client;
        }

        internal IKVEndpoint KvEndpoints => _client.KV;
        internal IAgentEndpoint Agent => _client.Agent;
    }
}
