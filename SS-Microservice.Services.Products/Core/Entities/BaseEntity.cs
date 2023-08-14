using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SS_Microservice.Services.Products.Core.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
    }
}