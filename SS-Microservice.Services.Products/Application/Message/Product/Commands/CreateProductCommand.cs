using MediatR;

namespace SS_Microservice.Services.Products.Application.Message.Product.Commands
{
    public class CreateProductCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public int Status { get; set; }

        public IFormFile Image { get; set; }

        public List<IFormFile> SubImages { get; set; }
    }
}