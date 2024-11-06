using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace HMS.Payments.Infrastructure.Serializer
{
    public sealed class ObjectIdStringSerializer : SerializerBase<string>
    {
        public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var objId = context.Reader.ReadObjectId();
            Console.WriteLine(objId.ToString() + "\n \n \n");
            return objId.ToString();
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
        {
            Console.WriteLine(value + " Teste \n \n \n");
            var objId = new ObjectId(value);
            context.Writer.WriteObjectId(objId);
        }
    }
}
