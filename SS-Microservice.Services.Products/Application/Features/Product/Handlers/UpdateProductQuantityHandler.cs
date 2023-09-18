using MediatR;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Product.Handlers
{
    public class UpdateProductQuantityHandler : IRequestHandler<UpdateProductQuantityCommand, bool>
    {
        private readonly IProductService _productService;

        public UpdateProductQuantityHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(UpdateProductQuantityCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProductQuantity(request);
        }
    }
}