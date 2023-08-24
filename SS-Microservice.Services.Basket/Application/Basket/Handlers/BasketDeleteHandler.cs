using MediatR;
using SS_Microservice.Services.Basket.Application.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Common.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Basket.Handlers
{
    public class BasketDeleteHandler : IRequestHandler<BasketDeleteCommand, bool>
    {
        private readonly IBasketService _basketService;

        public BasketDeleteHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<bool> Handle(BasketDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _basketService.RemoveProductFromBasket(request);
        }
    }
}