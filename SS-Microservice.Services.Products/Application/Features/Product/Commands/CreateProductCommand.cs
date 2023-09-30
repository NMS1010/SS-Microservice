using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class CreateProductCommand : CreateProductRequest, IRequest<bool>
    {
    }

    public class CreateProductHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private IProductService _productService;

        public CreateProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.AddProduct(request);
        }
    }
}