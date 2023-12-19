using AutoMapper;
using MassTransit;
using SS_Microservice.Contracts.Commands.Product;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.Inventory
{
    public class UpdateOneProductQuantityCommand : IUpdateProductQuantityCommand
    {
        public long ProductId { get; set; }
        public long Quantity { get; set; }
        public long ActualInventory { get; set; }

        public Guid CorrelationId { get; set; }
    }

    public class UpdateProductQuantityCommandConsumer : IConsumer<IUpdateProductQuantityCommand>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public UpdateProductQuantityCommandConsumer(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IUpdateProductQuantityCommand> context)
        {
            var message = _mapper.Map<UpdateOneProductQuantityCommand>(context.Message);

            await _productService.UpdateOneProductQuantity(message);
        }
    }
}
