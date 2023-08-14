using MediatR;
using SS_Microservice.Services.Products.Application.Dto;

namespace SS_Microservice.Services.Products.Application.Product.Queries
{
    public class ProductGetByIdQuery : IRequest<ProductDTO>
    {
        public Guid ProductId { get; set; }
    }
}