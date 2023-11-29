using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.ProductImage.Queries
{
    public class GetListProductImageQuery : IRequest<List<ProductImageDto>>
    {
        public long ProductId { get; set; }
    }

    public class GetListProductImageHandler : IRequestHandler<GetListProductImageQuery, List<ProductImageDto>>
    {
        private readonly IProductImageService _productImageService;

        public GetListProductImageHandler(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<List<ProductImageDto>> Handle(GetListProductImageQuery request, CancellationToken cancellationToken)
        {
            return await _productImageService.GetListProductImage(request);
        }
    }
}
