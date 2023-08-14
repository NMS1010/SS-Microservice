using MediatR;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Product.Queries;

namespace SS_Microservice.Services.Products.Application.Product.Handlers
{
    public class ProductGetByIdHandler : IRequestHandler<ProductGetByIdQuery, ProductDTO>
    {
        private IProductService _productService;

        public ProductGetByIdHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<ProductDTO> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            return _productService.GetProductById(request);
        }
    }
}