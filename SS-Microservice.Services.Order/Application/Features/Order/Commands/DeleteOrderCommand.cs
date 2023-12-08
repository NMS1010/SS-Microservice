using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Order.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderService _orderService;

        public DeleteOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            return _orderService.DeleteOrder(request);
        }
    }
}