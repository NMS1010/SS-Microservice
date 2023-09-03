using MongoDB.Driver;
using SS_Microservice.Services.Products.Core.Entities;
using SS_Microservice.Services.Products.Core.Interfaces;

namespace SS_Microservice.Services.Products.Infrastructure.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IMongoDBSettings mongoDBSettings)
        {
            var client = new MongoClient(mongoDBSettings.ConnectionString);
            Database = client.GetDatabase(mongoDBSettings.DatabaseName);
            Client = client;
        }

        public IMongoDatabase Database { get; }
        public IMongoClient Client { get; }
    }
}