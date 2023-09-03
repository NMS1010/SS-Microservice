using MediatR;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Message.Product.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public int Status { get; set; }

        public IFormFile Image { get; set; }

        public List<ProductSubImage> SubImages { get; set; }
    }
}