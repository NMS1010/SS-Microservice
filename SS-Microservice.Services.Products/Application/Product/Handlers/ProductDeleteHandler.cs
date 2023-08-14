using MediatR;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Application.Product.Commands;

namespace SS_Microservice.Services.Products.Application.Product.Handlers
{
    public class ProductDeleteHandler : IRequestHandler<ProductDeleteCommand, bool>
    {
        private IProductService _productService;

        public ProductDeleteHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProduct(request);
        }
    }
}