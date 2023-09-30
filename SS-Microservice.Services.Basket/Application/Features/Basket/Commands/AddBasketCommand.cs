using MediatR;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Commands
{
    public class AddBasketCommand : BasketAddRequest, IRequest
    {
    }

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