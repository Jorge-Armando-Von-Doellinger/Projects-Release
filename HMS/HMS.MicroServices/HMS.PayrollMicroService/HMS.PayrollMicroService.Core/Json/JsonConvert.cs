using System.Text;
using System.Text.Json;

namespace HMS.PayrollMicroService.Core.Json
{
    public static class JsonConvert
    {
        public static async Task<string> Serialize(object obj)
        {
            using(var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, obj);
                stream.Position = 0;
                using (var reader = new StreamReader(stream)) 
                    return await reader.ReadToEndAsync();
            }
        }

        public static async Task<T> Deserialize<T>(string json)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
}
