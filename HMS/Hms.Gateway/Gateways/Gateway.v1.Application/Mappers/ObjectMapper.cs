namespace Gateway.v1.Application.Mappers
{
    internal static class ObjectMapper
    {
        public static async Task<object> Mapper<Item>(Item item)
        {
            try
            {
                object value = item;
                return await Task.FromResult(value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<List<object>> Mapper<Item>(List<Item> itens)
        {
            try
            {
                var objects = new List<object>();
                foreach (var item in itens)
                {
                    objects.Add(await Mapper(item));
                }
                return objects;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
