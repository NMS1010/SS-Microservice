using AutoMapper;
using MediatR;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Application.Product.Commands;

namespace SS_Microservice.Services.Products.Application.Product.Handlers
{
    public class ProductCreateHandler : IRequestHandler<ProductCreateCommand>
    {
        private IProductService _productService;

        public ProductCreateHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            await _productService.AddProduct(request);
        }
    }
}