using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Queries
{
    public class GetListFilteringProductQuery : FilterProductPagingRequest, IRequest<PaginatedResult<ProductDto>>
    {
    }

    public class GetListFilteringProductHandler : IRequestHandler<GetListFilteringProductQuery, PaginatedResult<ProductDto>>
    {
        private readonly IProductService _productService;

        public GetListFilteringProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetListFilteringProductQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetListFilteringProduct(request);
        }
    }
}
