using MediatR;

namespace SS_Microservice.Services.Basket.Application.Message.Basket.Commands
{
    public class DeleteBasketCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public int BasketItemId { get; set; }
    }
}