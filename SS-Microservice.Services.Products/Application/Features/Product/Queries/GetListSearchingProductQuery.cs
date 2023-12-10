using MediatR;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Queries
{
    public class GetListSearchingProductQuery : SearchProductPagingRequest, IRequest<PaginatedResult<ProductDto>>
    {
    }

    public class GetListSearchingProductHandler : IRequestHandler<GetListSearchingProductQuery, PaginatedResult<ProductDto>>
    {
        private readonly IProductService _productService;

        public GetListSearchingProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetListSearchingProductQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetListSearchingProduct(request);
        }
    }
}
