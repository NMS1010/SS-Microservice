using MassTransit;
using SS_Microservice.Contracts.Commands.Product;
using SS_Microservice.Contracts.Models;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.UserOperation
{
    public class UpdateProductRatingCommand : IUpdateProductRatingCommand
    {
        public Guid CorrelationId { get; set; }

        public List<ProductRating> ProductRatings { get; set; }
    }

    public class UpdateProductRatingCommandConsumer : IConsumer<IUpdateProductRatingCommand>
    {
        private readonly IProductService _productService;

        public UpdateProductRatingCommandConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<IUpdateProductRatingCommand> context)
        {
            await _productService.UpdateProductRating(new UpdateProductRatingCommand()
            {
                ProductRatings = context.Message.ProductRatings
            });
        }
    }
}
