using MediatR;
using SS_Microservice.Services.Order.Application.Models.OrderState;

namespace SS_Microservice.Services.Order.Application.Features.OrderState.Commands
{
    public class CreateOrderStateCommand : CreateOrderStateRequest, IRequest
    {
    }
}