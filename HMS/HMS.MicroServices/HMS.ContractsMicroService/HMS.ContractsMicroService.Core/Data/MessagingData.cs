namespace HMS.ContractsMicroService.Core.Data
{
    public sealed class MessagingData
    {
        public MessagingData()
        {
            AddRetryCounter();
        }
        public readonly string _retryCountKey = "x-retry-count";

        public Dictionary<string, object> Header { get; private set; } = new();
        public string? Json { get; private set; } = null;
        public string? CurrentKey { get; private set; } = null;

        private void AddRetryCounter() => Header.Add(_retryCountKey, 0);

        public void AddRetry()
        {
            var value = Header[_retryCountKey];
            Header[_retryCountKey] = Convert.ToInt32(value) + 1;
        }
        public int GetAttempts() => Header.Count(x => x.Key == _retryCountKey);
        public void SetData(string json, string currentKey)
        {
            Json = json ?? Json;
            CurrentKey = currentKey ?? CurrentKey;
        }
    }
}
