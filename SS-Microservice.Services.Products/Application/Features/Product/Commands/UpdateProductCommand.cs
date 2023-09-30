using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class UpdateProductCommand : UpdateProductRequest, IRequest<bool>
    {
    }

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private IProductService _productService;

        public UpdateProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProduct(request);
        }
    }
}