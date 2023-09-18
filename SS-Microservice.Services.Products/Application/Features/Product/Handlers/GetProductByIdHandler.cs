using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Product.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private IProductService _productService;

        public GetProductByIdHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return _productService.GetProductById(request);
        }
    }
}