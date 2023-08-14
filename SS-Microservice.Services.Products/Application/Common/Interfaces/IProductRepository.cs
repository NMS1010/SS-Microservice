using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Products.Core.Entities;

namespace SS_Microservice.Services.Products.Application.Common.Interfaces
{
    public interface IProductRepository : IGenericRepository<Core.Entities.Product>
    {
    }
}