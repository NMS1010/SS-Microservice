using MediatR;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Application.Product.Commands;

namespace SS_Microservice.Services.Products.Application.Product.Handlers
{
    public class ProductImageUpdateHandler : IRequestHandler<ProductImageUpdateCommand, bool>
    {
        private readonly IProductService _productService;

        public ProductImageUpdateHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(ProductImageUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProductImage(request);
        }
    }
}