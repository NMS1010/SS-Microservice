using MediatR;

namespace SS_Microservice.Services.Order.Application.Features.OrderState.Commands
{
    public class DeleteOrderStateCommand : IRequest<bool>
    {
        public int OrderStateId { get; set; }
    }
}