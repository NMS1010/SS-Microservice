using MediatR;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Commands
{
    public class UpdateBasketItemCommand : UpdateBasketItemRequest, IRequest<bool>
    {
    }

    public class UpdateBasketItemHandler : IRequestHandler<UpdateBasketItemCommand, bool>
    {
        private readonly IBasketService _basketService;

        public UpdateBasketItemHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<bool> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            return await _basketService.UpdateBasketItem(request);
        }
    }
}