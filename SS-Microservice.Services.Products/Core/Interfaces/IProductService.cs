using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Message.Product.Commands;
using SS_Microservice.Services.Products.Application.Message.Product.Queries;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Core.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProduct(CreateProductCommand command);

        Task<bool> UpdateProduct(UpdateProductCommand command);

        Task<bool> UpdateProductQuantity(UpdateProductQuantityCommand command);

        Task<bool> UpdateProductImage(UpdateProductImageCommand command);

        Task<bool> DeleteProduct(DeleteProductCommand command);

        Task<bool> DeleteProductImage(DeleteProductImageCommand command);

        Task<PaginatedResult<ProductDTO>> GetAllProduct(GetAllProductQuery query);

        Task<ProductDTO> GetProductById(GetProductByIdQuery query);
    }
}