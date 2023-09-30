using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class CreateProductImageCommand : CreateProductImageRequest, IRequest<bool>
    {
    }

    public class CreateProductImageHandler : IRequestHandler<CreateProductImageCommand, bool>
    {
        private readonly IProductService _productService;

        public CreateProductImageHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            return await _productService.AddProductImage(request);
        }
    }
}