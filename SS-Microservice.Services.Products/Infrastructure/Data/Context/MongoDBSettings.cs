using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Infrastructure.Data.Context
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}