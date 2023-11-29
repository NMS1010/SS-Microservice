using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Product.Queries
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public long Id { get; set; }
    }

    public class GetProductHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IProductService _productService;

        public GetProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return _productService.GetProduct(request);
        }
    }
}