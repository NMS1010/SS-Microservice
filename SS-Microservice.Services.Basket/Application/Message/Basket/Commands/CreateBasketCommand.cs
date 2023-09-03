using MediatR;

namespace SS_Microservice.Services.Basket.Application.Message.Basket.Commands
{
    public class CreateBasketCommand : IRequest
    {
        public string UserId { get; set; }
    }
}