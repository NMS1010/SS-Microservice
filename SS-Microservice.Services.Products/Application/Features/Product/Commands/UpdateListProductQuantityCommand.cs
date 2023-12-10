using MediatR;
using SS_Microservice.Contracts.Models;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class UpdateListProductQuantityCommand : IRequest<bool>
    {
        public List<ProductStock> Products { get; set; }
    }

    public class UpdateProductQuantityHandler : IRequestHandler<UpdateListProductQuantityCommand, bool>
    {
        private readonly IProductService _productService;

        public UpdateProductQuantityHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(UpdateListProductQuantityCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateListProductQuantity(request);
        }
    }
}