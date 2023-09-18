using MediatR;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Features.Order.Commands
{
    public class CreateOrderCommand : CreateOrderRequest, IRequest<bool>
    {
    }
}