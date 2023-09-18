using MongoDB.Driver;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Data.Context
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