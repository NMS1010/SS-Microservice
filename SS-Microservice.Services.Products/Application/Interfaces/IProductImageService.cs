using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.ProductImage.Commands;
using SS_Microservice.Services.Products.Application.Features.ProductImage.Queries;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface IProductImageService
    {
        Task<List<ProductImageDto>> GetListProductImage(GetListProductImageQuery query);

        Task<ProductImageDto> GetProductImage(GetProductImageQuery query);

        Task<long> CreateProductImage(CreateProductImageCommand command);

        Task<bool> UpdateProductImage(UpdateProductImageCommand command);

        Task<bool> SetDefaultProductImage(SetDefaultProductImageCommand command);

        Task<bool> DeleteListProductImage(DeleteListProductImageCommand command);

        Task<bool> DeleteProductImage(DeleteProductImageCommand command);
    }
}