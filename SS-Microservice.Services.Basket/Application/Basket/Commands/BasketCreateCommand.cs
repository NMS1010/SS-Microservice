using MediatR;

namespace SS_Microservice.Services.Basket.Application.Basket.Commands
{
    public class BasketCreateCommand : IRequest
    {
        public string UserId { get; set; }
    }
}