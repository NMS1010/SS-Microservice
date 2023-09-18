using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;

namespace SS_Microservice.Services.Order.Application.Features.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public long OrderId { get; set; }
        public string UserId { get; set; }
    }
}