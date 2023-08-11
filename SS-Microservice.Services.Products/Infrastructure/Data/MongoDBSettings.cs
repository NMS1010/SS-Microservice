using SS_Microservice.Services.Products.Application.Common.Interfaces;

namespace SS_Microservice.Services.Products.Infrastructure.Data
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}