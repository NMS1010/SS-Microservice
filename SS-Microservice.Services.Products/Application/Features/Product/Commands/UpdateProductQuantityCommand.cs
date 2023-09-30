using MediatR;
using SS_Microservice.Common.Messages.Models;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class UpdateProductQuantityCommand : IRequest<bool>
    {
        public List<ProductStock> Products { get; set; }
    }

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