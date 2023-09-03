using MediatR;

namespace SS_Microservice.Services.Products.Application.Message.Product.Commands
{
    public class DeleteProductImageCommand : IRequest<bool>
    {
        public string ProductId { get; set; }
        public string ProductImageId { get; set; }
    }
}