using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Products.Core.Entities
{
    public class BaseEntity : AuditEntity
    {
        [BsonId]
        public string Id { get; set; }
    }
}