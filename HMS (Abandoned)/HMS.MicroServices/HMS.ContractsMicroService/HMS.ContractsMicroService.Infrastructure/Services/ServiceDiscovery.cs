﻿using Consul;
using HMS.ContractsMicroService.Core.Interfaces.Services;
using System.Text;
using System.Text.Json;

namespace HMS.ContractsMicroService.Infrastructure.Services
{
    public sealed class ServiceDiscovery : IServiceDiscovery
    {
        private readonly IConsulClient _client;

        public ServiceDiscovery(IConsulClient client)
        {
            _client = client;
        }

        public async Task Delete(string kvkey)
        {
            await _client.KV.Delete(kvkey);
        }

        public async Task<T> Get<T>(string kvkey)
        {
            var bytes = (await _client.KV.Get(kvkey)).Response.Value ?? throw new InvalidKeyPairException("Invalid kvKey");
            var data = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<T>(bytes) ?? throw new InvalidCastException("Types isn't compatible");
        }

        public async Task<string> Get(string kvkey)
        {
            var bytes = (await _client.KV.Get(kvkey)).Response.Value ?? throw new InvalidKeyPairException("Invalid kvKey");
            var data = Encoding.UTF8.GetString(bytes);
            return data;
        }

        public async Task Put(object settings, string kvkey)
        {
            string? json;
            try
            {
                json = JsonSerializer.Serialize(settings);
            }
            catch
            {
                json = (string)settings;
            }
            if (json == null) throw new Exception("Can't put the settings");
            var bytes = Encoding.UTF8.GetBytes(json);
            KVPair kVPair = new (kvkey) { Value = bytes };
            await _client.KV.Put(kVPair);
        }

        public async Task Register(object settings, string kvkey)
        {
            await Put(settings, kvkey);
        }
    }
}
