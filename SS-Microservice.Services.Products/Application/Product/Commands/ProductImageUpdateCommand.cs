using MediatR;

namespace SS_Microservice.Services.Products.Application.Product.Commands
{
    public class ProductImageUpdateCommand : IRequest<bool>
    {
        public string ProductId { get; set; }
        public string ProductImageId { get; set; }
        public IFormFile Image { get; set; }
    }
}