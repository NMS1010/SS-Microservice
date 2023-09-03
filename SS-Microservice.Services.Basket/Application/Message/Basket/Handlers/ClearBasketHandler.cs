using MediatR;
using SS_Microservice.Services.Basket.Application.Message.Basket.Commands;
using SS_Microservice.Services.Basket.Core.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Message.Basket.Handlers
{
    public class ClearBasketHandler : IRequestHandler<ClearBasketCommand, bool>
    {
        private readonly IBasketService _basketService;

        public ClearBasketHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<bool> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
        {
            return await _basketService.ClearBasket(request);
        }
    }
}