using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace HMS.Payments.Infrastructure.Generator
{
    public sealed class StringIdGenerator : IIdGenerator
    {
        public object GenerateId(object container, object document)
        {
            return ObjectId.GenerateNewId().ToString();
        }

        public bool IsEmpty(object id)
        {
            return string.IsNullOrEmpty(id as string);
        }
    }
}
