using MediatR;

namespace SS_Microservice.Services.Order.Application.Features.Order.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public long OrderId { get; set; }
    }
}