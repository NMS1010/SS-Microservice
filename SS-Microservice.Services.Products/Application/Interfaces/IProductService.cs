using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.Inventory;
using SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.OrderingSaga;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDto>> GetListProduct(GetListProductQuery query);

        Task<PaginatedResult<ProductDto>> GetListFilteringProduct(GetListFilteringProductQuery query);

        Task<PaginatedResult<ProductDto>> GetListSearchingProduct(GetListSearchingProductQuery query);

        Task<ProductDto> GetProduct(GetProductQuery query);

        Task<ProductDto> GetProductBySlug(GetProductBySlugQuery query);

        Task<long> CreateProduct(CreateProductCommand command);

        Task<bool> UpdateProduct(UpdateProductCommand command);

        Task<bool> DeleteProduct(DeleteProductCommand command);

        Task<bool> DeleteListProduct(DeleteListProductCommand command);

        // messaging

        Task UpdateOneProductQuantity(UpdateOneProductQuantityCommand command);

        Task<bool> ReserveStock(ReserveStockCommand command);

        Task RollBackStock(RollBackStockCommand command);
    }
}