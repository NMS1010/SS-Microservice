using MediatR;
using SS_Microservice.Services.Basket.Application.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Common.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Basket.Handlers
{
    public class BasketAddHandler : IRequestHandler<BasketAddCommand>
    {
        private readonly IBasketService _basketService;

        public BasketAddHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task Handle(BasketAddCommand request, CancellationToken cancellationToken)
        {
            await _basketService.AddProductToBasket(request);
        }
    }
}