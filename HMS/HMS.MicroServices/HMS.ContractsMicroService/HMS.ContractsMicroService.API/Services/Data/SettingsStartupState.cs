using Consul;

namespace HMS.ContractsMicroService.API.Services.Data
{
    public class SettingsStartupState
    {
        public SettingsStartupState()
        {
           
        }
        private static readonly TaskCompletionSource<bool> _taskCompletionSource = new();

        internal static void SetSettingsCompleted()
        {
            _taskCompletionSource.SetResult(true);
        }

        internal static Task AwaitSettingsCompletionAsync()
        {
            return _taskCompletionSource.Task;
        }
    }
}
