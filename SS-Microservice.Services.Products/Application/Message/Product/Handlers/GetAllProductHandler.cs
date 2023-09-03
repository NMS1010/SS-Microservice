using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Message.Product.Queries;
using SS_Microservice.Services.Products.Core.Interfaces;

namespace SS_Microservice.Services.Products.Application.Message.Product.Handlers
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, PaginatedResult<ProductDTO>>
    {
        private IProductService _productService;

        public GetAllProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<PaginatedResult<ProductDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllProduct(request);
        }
    }
}