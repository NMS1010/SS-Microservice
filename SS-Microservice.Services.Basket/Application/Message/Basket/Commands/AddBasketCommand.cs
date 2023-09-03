using MediatR;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Message.Basket.Commands
{
    public class AddBasketCommand : BasketAddRequest, IRequest
    {
    }
}