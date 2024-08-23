namespace Nuget.Response
{
    public class Response
    {
        public string Message { get; set; }
        public object? Content { get; set; }
        public bool Success { get; set; } = true;
        public List<string> Errors { get; set; }

        public void AddErrors(string error)
        {
            if (Errors == null)
                Errors = new List<string>();
            if(error != null)
                Errors.Add(error);
        }
        public void CaseError(string message, string error = null)
        {
            Success = false;
            AddErrors(error);
            Message = message;
        }
    }
    public class Response<T>
    {
        public string Message { get; set; }
        public T? Content { get; set; }
        public bool Success { get; set; } = true;
        public List<string> Errors { get; set; }

        public void AddErrors(string error)
        {
            if (Errors == null)
                Errors = new List<string>();
            if (error != null)
                Errors.Add(error);
        }
        public void CaseError(string message, string error = null)
        {
            Success = false;
            AddErrors(error);
            Message = message;
        }
    }
}
