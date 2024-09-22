namespace Nuget.Response.Services
{
    public static class ResponseService
    {
        public static Response ResponseError(string message, string error)
        {
            return new()
            {
                Message = message,
                Errors = new() { error },
                Success = false
            };
        }
        public static Response ResponseError(string message, List<string> errors = null)
        {
            return new()
            {
                Message = message,
                Errors = errors ?? new(),
                Success = false
            };
        }

        public static Response ResponseSuccess(string? message = null, object? content = null)
        {
            return new()
            {
                Message = message ?? string.Empty,
                Content = content,
            };
        }
    }
}
