using MediatR;

namespace SS_Microservice.Services.Products.Application.Message.Product.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public string ProductId { get; set; }
    }
}