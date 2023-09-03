using SS_Microservice.Services.Products.Core.Interfaces;

namespace SS_Microservice.Services.Products.Infrastructure.Data
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}