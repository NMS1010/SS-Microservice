using MediatR;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public string ProductId { get; set; }
    }
}