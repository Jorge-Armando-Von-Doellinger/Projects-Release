using Nuget.Client.Output;
using System.Text;
using System.Text.Json;

namespace Gateway.v1.Application.Desserializer
{
    public static class JsonDeserializer
    {
        public static async Task<T> Deserialize<T>(string json)
        {
            try
            {
                //Console.WriteLine(json);
                using(MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    //var Json = JsonSerializer.Deserialize<T>(json);
                    
                    return await JsonSerializer.DeserializeAsync<T>(stream) 
                        ?? throw new Exception("Erro ao desserializar dados!");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
