using MediatR;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Handlers
{
    public class DeleteBasketHandler : IRequestHandler<DeleteBasketCommand, bool>
    {
        private readonly IBasketService _basketService;

        public DeleteBasketHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            return await _basketService.RemoveProductFromBasket(request);
        }
    }
}