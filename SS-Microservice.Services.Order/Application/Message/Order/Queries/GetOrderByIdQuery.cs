using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;

namespace SS_Microservice.Services.Order.Application.Message.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
    }
}