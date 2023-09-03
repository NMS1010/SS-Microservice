using MediatR;
using SS_Microservice.Services.Basket.Application.Message.Basket.Commands;
using SS_Microservice.Services.Basket.Core.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Message.Basket.Handlers
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