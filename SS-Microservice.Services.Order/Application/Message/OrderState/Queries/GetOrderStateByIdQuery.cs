using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;

namespace SS_Microservice.Services.Order.Application.Message.OrderState.Queries
{
    public class GetOrderStateByIdQuery : IRequest<OrderStateDto>
    {
        public int OrderStateId { get; set; }
    }
}