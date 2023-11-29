using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.ProductImage;

namespace SS_Microservice.Services.Products.Application.Features.ProductImage.Commands
{
    public class CreateProductImageCommand : CreateProductImageRequest, IRequest<long>
    {
    }

    public class CreateProductImageHandler : IRequestHandler<CreateProductImageCommand, long>
    {
        private readonly IProductImageService _productImageService;

        public CreateProductImageHandler(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<long> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            return await _productImageService.CreateProductImage(request);
        }
    }
}
