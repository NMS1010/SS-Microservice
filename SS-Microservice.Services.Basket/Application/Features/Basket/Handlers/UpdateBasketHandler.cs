using MediatR;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Handlers
{
    public class UpdateBasketHandler : IRequestHandler<UpdateBasketCommand, bool>
    {
        private readonly IBasketService _basketService;

        public UpdateBasketHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<bool> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            return await _basketService.UpdateProductQuantity(request);
        }
    }
}