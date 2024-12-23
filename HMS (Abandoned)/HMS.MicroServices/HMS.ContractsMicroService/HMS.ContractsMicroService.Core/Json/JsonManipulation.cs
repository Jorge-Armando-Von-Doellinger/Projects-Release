﻿using System.Text;
using System.Text.Json;

namespace HMS.ContractsMicroService.Core.Json
{
    public static class JsonManipulation
    {
        public static async Task<string> Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, obj);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        public static async Task<TTarget> Deserialize<TTarget>(string json)
        {
            return (TTarget)await Deserialize(json, typeof(TTarget));
        }
        public static async Task<object> Deserialize(string json, Type type)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                return await JsonSerializer.DeserializeAsync(stream, type);
        }
    }
}
