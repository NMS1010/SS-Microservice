using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProduct(CreateProductCommand command);

        Task<bool> UpdateProduct(UpdateProductCommand command);

        Task<bool> UpdateProductQuantity(UpdateProductQuantityCommand command);

        Task<bool> DeleteProduct(DeleteProductCommand command);

        Task<PaginatedResult<ProductDto>> GetAllProduct(GetAllProductQuery query);

        Task<PaginatedResult<ProductImageDto>> GetAllProductImage(GetAllProductImageQuery query);

        Task<ProductDto> GetProductById(GetProductByIdQuery query);

        Task<bool> AddProductImage(CreateProductImageCommand command);

        Task<bool> UpdateProductImage(UpdateProductImageCommand command);

        Task<bool> DeleteProductImage(DeleteProductImageCommand command);
    }
}