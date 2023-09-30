using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands
{
    public class DeletePaymentMethodCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeletePaymentMethodHandler : IRequestHandler<DeletePaymentMethodCommand, bool>
    {
        private readonly IPaymentMethodService _service;

        public DeletePaymentMethodHandler(IPaymentMethodService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeletePaymentMethod(request);
        }
    }
}