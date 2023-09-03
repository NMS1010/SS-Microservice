using MediatR;

namespace SS_Microservice.Services.Products.Application.Message.Product.Commands
{
    public class UpdateProductImageCommand : IRequest<bool>
    {
        public string ProductId { get; set; }
        public string ProductImageId { get; set; }
        public IFormFile Image { get; set; }
    }
}