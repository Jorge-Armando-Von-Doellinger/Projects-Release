using System.Security.Cryptography.X509Certificates;

namespace HMS.ContractsMicroService.Core.Data
{
    public sealed class MessagingData
    {
        public string? Json { get; private set; } = null;
        public string? CurrentKey { get; private set; } = null;
        public void SetData(string json, string currentKey)
        {
            Json = json ?? Json;
            CurrentKey = currentKey ?? CurrentKey;
        }

    }
}
