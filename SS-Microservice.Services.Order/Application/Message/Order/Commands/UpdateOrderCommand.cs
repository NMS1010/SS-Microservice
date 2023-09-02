using MediatR;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Message.Order.Commands
{
    public class UpdateOrderCommand : OrderUpdateRequest, IRequest<bool>
    {
    }
}