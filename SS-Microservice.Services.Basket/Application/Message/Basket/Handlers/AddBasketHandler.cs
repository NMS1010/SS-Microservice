using MediatR;
using SS_Microservice.Services.Basket.Application.Message.Basket.Commands;
using SS_Microservice.Services.Basket.Core.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Message.Basket.Handlers
{
    public class AddBasketHandler : IRequestHandler<AddBasketCommand>
    {
        private readonly IBasketService _basketService;

        public AddBasketHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task Handle(AddBasketCommand request, CancellationToken cancellationToken)
        {
            await _basketService.AddProductToBasket(request);
        }
    }
}