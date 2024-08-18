using System.Text;
using System.Text.Json;

namespace Gateway.v1.Messaging.Serializer
{
    internal static class DataSerializer
    {
        internal static async Task<string> Serialize(object data)
        {
            try
            {
                using(var stream = new MemoryStream())
                {
                    await JsonSerializer.SerializeAsync(stream, data);
                    stream.Seek(0, SeekOrigin.Begin);
                    using(var reader = new StreamReader(stream, Encoding.UTF8, false))
                    {
                        string dataSerialized = await reader.ReadToEndAsync();
                        return dataSerialized;
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
