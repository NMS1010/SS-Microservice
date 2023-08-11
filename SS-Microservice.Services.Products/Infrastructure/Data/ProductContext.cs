using MongoDB.Driver;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Core.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IMongoDBSettings mongoDBSettings)
        {
            var client = new MongoClient(mongoDBSettings.ConnectionString);
            Database = client.GetDatabase(mongoDBSettings.DatabaseName);
        }

        public IMongoDatabase Database { get; }
    }
}