using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SS_Microservice.Services.Products.Core.Entities
{
    public class BaseEntity
    {
        [BsonId]
        public string Id { get; set; }
    }
}