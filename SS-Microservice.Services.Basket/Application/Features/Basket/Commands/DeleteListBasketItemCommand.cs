using MediatR;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Commands
{
    public class DeleteListBasketItemCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
        public string UserId { get; set; }
    }

    public class DeleteListBasketItemHandler : IRequestHandler<DeleteListBasketItemCommand, bool>
    {
        private readonly IBasketService _basketService;

        public DeleteListBasketItemHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<bool> Handle(DeleteListBasketItemCommand request, CancellationToken cancellationToken)
        {
            return await _basketService.DeleteListBasketItem(request);
        }
    }
}
