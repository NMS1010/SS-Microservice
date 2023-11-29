using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Queries
{
    public class GetListProductQuery : GetProductPagingRequest, IRequest<PaginatedResult<ProductDto>>
    {
    }

    public class GetAllProductHandler : IRequestHandler<GetListProductQuery, PaginatedResult<ProductDto>>
    {
        private readonly IProductService _productService;

        public GetAllProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetListProduct(request);
        }
    }
}