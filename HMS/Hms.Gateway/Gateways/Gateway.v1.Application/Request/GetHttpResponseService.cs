using Nuget.Client.Output;
using System.Text.Json;

namespace Gateway.v1.Application.Request
{
    public static class GetHttpResponseService
    {
        public static async Task<string> GetResponseMessage(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();
                    throw new Exception("Houve um erro durante a requisição!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n HttpService \n" + ex.Message);
                throw;
            }
        }
    }
}
