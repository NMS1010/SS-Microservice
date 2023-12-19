using AutoMapper;
using MassTransit;
using SS_Microservice.Contracts.Commands.Product;
using SS_Microservice.Contracts.Models;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.OrderingSaga
{
    public class RollBackStockCommand : IRollBackStockCommand
    {
        public List<ProductStock> Stocks { get; set; }

        public Guid CorrelationId { get; set; }
    }

    public class RollBackStockCommandConsumer : IConsumer<IRollBackStockCommand>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public RollBackStockCommandConsumer(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IRollBackStockCommand> context)
        {
            await _productService.RollBackStock(_mapper.Map<RollBackStockCommand>(context.Message));
        }
    }
}
