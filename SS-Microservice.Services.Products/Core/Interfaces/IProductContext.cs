using MongoDB.Driver;
using SS_Microservice.Services.Products.Core.Entities;

namespace SS_Microservice.Services.Products.Core.Interfaces
{
    public interface IProductContext
    {
        IMongoDatabase Database { get; }
        IMongoClient Client { get; }
    }
}