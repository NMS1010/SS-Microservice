using MediatR;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Handlers
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketCommand>
    {
        private readonly IBasketService _basketService;

        public CreateBasketHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            await _basketService.CreateBasket(request);
        }
    }
}