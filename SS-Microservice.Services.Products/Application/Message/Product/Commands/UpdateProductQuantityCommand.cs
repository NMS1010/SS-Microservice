using MediatR;
using SS_Microservice.Common.Messages.Models;

namespace SS_Microservice.Services.Products.Application.Message.Product.Commands
{
    public class UpdateProductQuantityCommand : IRequest<bool>
    {
        public List<ProductStock> Products { get; set; }
    }
}