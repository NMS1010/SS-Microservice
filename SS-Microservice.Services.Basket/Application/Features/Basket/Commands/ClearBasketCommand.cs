using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Commands
{
    public class ClearBasketCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public List<string> VariantIds { get; set; }
    }

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