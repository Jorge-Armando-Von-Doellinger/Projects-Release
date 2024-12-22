using System.Text;
using System.Text.Json;

namespace HMS.Core.Json
{
    public sealed class JsonService
    {
        public static async Task<T> DeserializeAsync<T>(string json)
        {
            try
            {
                using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    return await JsonSerializer.DeserializeAsync<T>(stream);
                }
            }
            catch
            {
                return default;
            }
        }
        public static async Task<T> DeserializeAsync<T>(JsonElement jsonElement)
        {
            try
            {
                var jsonString = jsonElement.GetRawText();
                using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    return await JsonSerializer.DeserializeAsync<T>(stream);
                }
            }
            catch
            {
                return default;
            }
        }
        public static async Task<T> DeserializeAsync<T>(object obj)
        {
            try
            {
                if(obj is JsonElement)
                    return await DeserializeAsync<T>((JsonElement) obj);
                else if(obj is T)
                    return (T) obj;
                else if(obj is string)
                    return await DeserializeAsync<T>((string) obj);
                throw new Exception("Erro ao desserializar objeto....");
            }
            catch(Exception ex)
            {
                return default;
            }
        }

        public static async Task<string> SerializeAsync(object obj)
        {
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
