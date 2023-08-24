using MediatR;
using SS_Microservice.Services.Basket.Application.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Common.Interfaces;
using SS_Microservice.Services.Basket.Application.Dto;

namespace SS_Microservice.Services.Basket.Application.Basket.Handlers
{
    public class BasketGetHandler : IRequestHandler<BasketGetQuery, BasketDto>
    {
        private readonly IBasketService _basketService;

        public BasketGetHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<BasketDto> Handle(BasketGetQuery request, CancellationToken cancellationToken)
        {
            return await _basketService.GetBasket(request);
        }
    }
}