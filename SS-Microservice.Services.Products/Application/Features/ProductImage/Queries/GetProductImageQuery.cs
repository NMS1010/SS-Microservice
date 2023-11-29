using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.ProductImage.Queries
{
    public class GetProductImageQuery : IRequest<ProductImageDto>
    {
        public long Id { get; set; }
    }

    public class GeProductImageHandler : IRequestHandler<GetProductImageQuery, ProductImageDto>
    {
        private readonly IProductImageService _productImageService;

        public GeProductImageHandler(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<ProductImageDto> Handle(GetProductImageQuery request, CancellationToken cancellationToken)
        {
            return await _productImageService.GetProductImage(request);
        }
    }
}
