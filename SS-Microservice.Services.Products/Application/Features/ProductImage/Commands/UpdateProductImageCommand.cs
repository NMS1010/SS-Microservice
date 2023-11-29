using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.ProductImage;

namespace SS_Microservice.Services.Products.Application.Features.ProductImage.Commands
{
    public class UpdateProductImageCommand : UpdateProductImageRequest, IRequest<bool>
    {
    }

    public class UpdateProductImageHandler : IRequestHandler<UpdateProductImageCommand, bool>
    {
        private readonly IProductImageService _productImageService;

        public UpdateProductImageHandler(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<bool> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            return await _productImageService.UpdateProductImage(request);
        }
    }
}
