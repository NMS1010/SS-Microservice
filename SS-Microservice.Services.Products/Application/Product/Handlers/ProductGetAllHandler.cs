using MediatR;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Product.Queries;

namespace SS_Microservice.Services.Products.Application.Product.Handlers
{
    public class ProductGetAllHandler : IRequestHandler<ProductGetAllQuery, List<ProductDTO>>
    {
        private IProductService _productService;

        public ProductGetAllHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<ProductDTO>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllProduct(request);
        }
    }
}