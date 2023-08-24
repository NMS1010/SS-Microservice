using MediatR;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Basket.Commands
{
    public class BasketAddCommand : BasketAddRequest, IRequest
    {
    }
}