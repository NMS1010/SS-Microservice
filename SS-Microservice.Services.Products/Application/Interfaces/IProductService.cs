using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;

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

        Task<bool> UpdateProductQuantity(UpdateProductQuantityCommand command);
    }
}