using MediatR;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Application.Product.Commands;

namespace SS_Microservice.Services.Products.Application.Product.Handlers
{
    public class ProductImageDeleteHandler : IRequestHandler<ProductImageDeleteCommand, bool>
    {
        private readonly IProductService _productService;

        public ProductImageDeleteHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(ProductImageDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProductImage(request);
        }
    }
}