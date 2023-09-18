using MediatR;
using SS_Microservice.Services.Products.Application.Dto;

namespace SS_Microservice.Services.Products.Application.Features.Product.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public string ProductId { get; set; }
    }
}