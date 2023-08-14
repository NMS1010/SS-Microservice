using MediatR;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Application.Product.Commands;

namespace SS_Microservice.Services.Products.Application.Product.Handlers
{
    public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, bool>
    {
        private IProductService _productService;

        public ProductUpdateHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProduct(request);
        }
    }
}