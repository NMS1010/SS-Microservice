using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands
{
    public class DeleteListPaymentMethodCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }

    public class DeleteListPaymentMethodHandler : IRequestHandler<DeleteListPaymentMethodCommand, bool>
    {
        private readonly IPaymentMethodService _service;

        public DeleteListPaymentMethodHandler(IPaymentMethodService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteListPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteListPaymentMethod(request);
        }
    }
}
