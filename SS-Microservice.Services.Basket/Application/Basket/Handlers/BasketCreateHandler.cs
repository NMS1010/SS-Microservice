using MediatR;
using SS_Microservice.Services.Basket.Application.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Common.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Basket.Handlers
{
    public class BasketCreateHandler : IRequestHandler<BasketCreateCommand>
    {
        private readonly IBasketService _basketService;

        public BasketCreateHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task Handle(BasketCreateCommand request, CancellationToken cancellationToken)
        {
            await _basketService.CreateBasket(request);
        }
    }
}