using MongoDB.Driver;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Products.Application.Message.Product.Commands;
using SS_Microservice.Services.Products.Application.Message.Product.Queries;
using SS_Microservice.Services.Products.Core.Entities;

namespace SS_Microservice.Services.Products.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PaginatedResult<Product>> FilterProduct(GetAllProductQuery query);

        Task<bool> UpdateProductQuantity(UpdateProductQuantityCommand command);
    }
}