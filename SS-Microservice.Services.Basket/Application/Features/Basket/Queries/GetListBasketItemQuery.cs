using MediatR;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Queries
{
    public class GetListBasketItemQuery : IRequest<List<BasketItemDto>>
    {
        public List<long> Ids { get; set; }
        public string UserId { get; set; }
    }

    public class GetListBasketItemQueryHandler : IRequestHandler<GetListBasketItemQuery, List<BasketItemDto>>
    {
        private readonly IBasketService _basketService;

        public GetListBasketItemQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<BasketItemDto>> Handle(GetListBasketItemQuery request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItemByIds(request);

            return basketItems;
        }
    }
}
