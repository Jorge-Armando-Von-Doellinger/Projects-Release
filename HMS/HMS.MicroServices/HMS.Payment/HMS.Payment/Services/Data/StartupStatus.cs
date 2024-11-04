namespace HMS.Payments.API.Services.Data
{
    public static class StartupStatus
    {
        private static TaskCompletionSource<bool> _startupCompleted = new();
        internal static void SetCompletedStartup()
        {
            _startupCompleted.SetResult(true);
        }
        internal static Task AwaitCompleteStartup() => _startupCompleted.Task;
    }
}
