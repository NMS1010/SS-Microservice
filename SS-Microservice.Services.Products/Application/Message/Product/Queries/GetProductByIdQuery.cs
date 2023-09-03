using MediatR;
using SS_Microservice.Services.Products.Application.Dto;

namespace SS_Microservice.Services.Products.Application.Message.Product.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public string ProductId { get; set; }
    }
}