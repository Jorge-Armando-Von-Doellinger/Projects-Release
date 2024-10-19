namespace HMS.ContractsMicroService.API.Services.Data
{
    public class SettingsStartupState
    {
        public SettingsStartupState()
        {
            _taskCompletionSource.SetResult(false);
        }
        private static readonly TaskCompletionSource<bool> _taskCompletionSource = new();

        internal static void SetSettingsCompleted()
        {
            _taskCompletionSource.SetResult(true);
        }

        internal static Task AwaitSettingsCompletionAsync()
        {
            if (_taskCompletionSource.Task.IsCompleted)
                return _taskCompletionSource.Task;
            return Task.Delay(Timeout.Infinite);
        }
    }
}
