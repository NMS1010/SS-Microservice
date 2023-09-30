using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class UpdateProductImageCommand : UpdateProductImageRequest, IRequest<bool>
    {
    }

    public class UpdateProductImageHandler : IRequestHandler<UpdateProductImageCommand, bool>
    {
        private readonly IProductService _productService;

        public UpdateProductImageHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProductImage(request);
        }
    }
}