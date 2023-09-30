using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Queries
{
    public class GetAllProductImageQuery : GetProductImagePagingRequest, IRequest<PaginatedResult<ProductImageDto>>
    {
    }

    public class GetAllProductImageHandler : IRequestHandler<GetAllProductImageQuery, PaginatedResult<ProductImageDto>>
    {
        private IProductService _productService;

        public GetAllProductImageHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<PaginatedResult<ProductImageDto>> Handle(GetAllProductImageQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllProductImage(request);
        }
    }
}