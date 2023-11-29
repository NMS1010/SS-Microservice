using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Product.Queries
{
    public class GetProductBySlugQuery : IRequest<ProductDto>
    {
        public string Slug { get; set; }
    }

    public class GetProductBySlugHandler : IRequestHandler<GetProductBySlugQuery, ProductDto>
    {
        private readonly IProductService _productService;

        public GetProductBySlugHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<ProductDto> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
        {
            return _productService.GetProductBySlug(request);
        }
    }
}
