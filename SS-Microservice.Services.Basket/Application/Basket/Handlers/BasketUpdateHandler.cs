using MediatR;
using SS_Microservice.Services.Basket.Application.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Common.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Basket.Handlers
{
    public class BasketUpdateHandler : IRequestHandler<BasketUpdateCommand, bool>
    {
        private readonly IBasketService _basketService;

        public BasketUpdateHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<bool> Handle(BasketUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _basketService.UpdateProductQuantity(request);
        }
    }
}