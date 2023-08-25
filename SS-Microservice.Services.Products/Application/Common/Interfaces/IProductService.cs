using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Model.Product;
using SS_Microservice.Services.Products.Application.Product.Commands;
using SS_Microservice.Services.Products.Application.Product.Queries;

namespace SS_Microservice.Services.Products.Application.Common.Interfaces
{
    public interface IProductService
    {
        Task AddProduct(ProductCreateCommand command);

        Task<bool> UpdateProduct(ProductUpdateCommand command);

        Task<bool> UpdateProductImage(ProductImageUpdateCommand command);

        Task<bool> DeleteProduct(ProductDeleteCommand command);

        Task<bool> DeleteProductImage(ProductImageDeleteCommand command);

        Task<PaginatedResult<ProductDTO>> GetAllProduct(ProductGetAllQuery query);

        Task<ProductDTO> GetProductById(ProductGetByIdQuery query);
    }
}