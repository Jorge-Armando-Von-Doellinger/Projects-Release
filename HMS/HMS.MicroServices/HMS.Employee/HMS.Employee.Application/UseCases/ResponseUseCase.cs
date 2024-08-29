using Nuget.Response;

namespace HMS.Employee.Application.Response
{
    internal static class ResponseUseCase
    {
        private async static Task<Nuget.Response.Response> GetResponse()
        {
            return await Task.Run(() => new Nuget.Response.Response());
        }
        internal async static Task<Nuget.Response.Response> GetResponseSuccess(string message= null, object content = null)
        {
            var response = await GetResponse();
            await Task.Run(() =>
            {
                response.Message = message;
                response.Content = content;
            });
            return response;
        }

        internal async static Task<Nuget.Response.Response> GetResponseError(string message, List<string> errors = null)
        {
            var response = await GetResponse();
            await Task.Run(() =>
            {
                response.Success = false;
                response.Message = message;
                response.Errors = errors;
            });
            return response;
        }


    }
}
