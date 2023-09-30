using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Products.Domain.Entities
{
    public class BaseMongoEntity : BaseAuditableEntity<string>
    {
        //[BsonId]
        //public new string Id { get; set; }
    }
}