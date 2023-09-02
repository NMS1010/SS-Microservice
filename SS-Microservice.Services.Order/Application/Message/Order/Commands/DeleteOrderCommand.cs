using MediatR;

namespace SS_Microservice.Services.Order.Application.Message.Order.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public string OrderId { get; set; }
    }
}