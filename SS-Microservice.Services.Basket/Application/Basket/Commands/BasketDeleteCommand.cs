using MediatR;

namespace SS_Microservice.Services.Basket.Application.Basket.Commands
{
    public class BasketDeleteCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public int BasketItemId { get; set; }
    }
}