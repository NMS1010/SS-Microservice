using MediatR;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Commands
{
    public class CreateBasketItemCommand : CreateBasketItemRequest, IRequest<bool>
    {
    }

    public class CreateBasketItemHandler : IRequestHandler<CreateBasketItemCommand, bool>
    {
        private readonly IBasketService _basketService;

        public CreateBasketItemHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<bool> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            return await _basketService.CreateBasketItem(request);
        }
    }
}