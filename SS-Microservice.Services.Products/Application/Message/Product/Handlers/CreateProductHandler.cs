using AutoMapper;
using MediatR;
using SS_Microservice.Services.Products.Application.Message.Product.Commands;
using SS_Microservice.Services.Products.Core.Interfaces;

namespace SS_Microservice.Services.Products.Application.Message.Product.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand>
    {
        private IProductService _productService;

        public CreateProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _productService.AddProduct(request);
        }
    }
}