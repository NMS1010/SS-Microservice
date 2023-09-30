using MongoDB.Driver;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PaginatedResult<Product>> FilterProduct(GetAllProductQuery query);

        Task<bool> UpdateProductQuantity(UpdateProductQuantityCommand command);

        Task<Product> GetProductByVariant(string variantId);
    }
}