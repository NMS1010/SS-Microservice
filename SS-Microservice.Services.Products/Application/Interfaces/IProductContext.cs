using MongoDB.Driver;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface IProductContext
    {
        IMongoDatabase Database { get; }
        IMongoClient Client { get; }
    }
}