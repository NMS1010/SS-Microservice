using MediatR;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Commands
{
    public class DeleteBasketItemCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public long CartItemId { get; set; }
    }

    public class DeleteBasketItemHandler : IRequestHandler<DeleteBasketItemCommand, bool>
    {
        private readonly IBasketService _basketService;

        public DeleteBasketItemHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<bool> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            return await _basketService.DeleteBasketItem(request);
        }
    }
}