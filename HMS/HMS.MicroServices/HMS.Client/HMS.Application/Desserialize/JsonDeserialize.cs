using Nuget.Client.Input;
using System.Text;
using System.Text.Json;

namespace HMS.Application.Desserialize
{
    internal static class JsonDeserialize
    {
        internal static async Task<T> Deserialize<T>(JsonElement jsonElement)
        {
            try
            {
                var jsonstring = jsonElement.GetRawText();
                
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonstring)) )
                {
                    
                    return await JsonSerializer.DeserializeAsync<T>(stream);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        internal static async Task<T> Deserialize<T>(object data)
        {
            try
            {
                if(data is JsonElement)
                {
                    var jsonElement = (JsonElement) data;
                    return await Deserialize<T>(jsonElement);
                }
                else if(data is T)
                    return (T) data;
                throw new Exception("Erro ao desserializar objeto!");
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
