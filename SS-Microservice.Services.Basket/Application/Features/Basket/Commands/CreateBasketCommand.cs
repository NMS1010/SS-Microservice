using MediatR;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Commands
{
    public class CreateBasketCommand : IRequest
    {
        public string UserId { get; set; }
    }

    public class AddBasketHandler : IRequestHandler<CreateBasketCommand>
    {
        private readonly IBasketService _basketService;

        public AddBasketHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            await _basketService.CreateBasket(request);
        }
    }
}