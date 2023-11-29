using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class CreateProductCommand : CreateProductRequest, IRequest<long>
    {
    }

    public class CreateProductHandler : IRequestHandler<CreateProductCommand, long>
    {
        private readonly IProductService _productService;

        public CreateProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.CreateProduct(request);
        }
    }
}