using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HMS.Core.Json
{
    public sealed class JsonService
    {
        public static async Task<T> DeserializeAsync<T>(string json)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    Converters = {
                        new GuidConverter()
                        }
                };
                using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    return await JsonSerializer.DeserializeAsync<T>(stream, options);
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
                var options = new JsonSerializerOptions
                {
                    Converters = {
                        new GuidConverter()
                        }
                };
                var jsonString = jsonElement.GetRawText();
                using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    return await JsonSerializer.DeserializeAsync<T>(stream, options);
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
