using MediatR;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Commands
{
    public class UpdateBasketCommand : BasketUpdateRequest, IRequest<bool>
    {
    }
}