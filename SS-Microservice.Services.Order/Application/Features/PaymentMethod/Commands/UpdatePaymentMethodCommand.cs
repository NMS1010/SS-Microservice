using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.PaymentMethod;

namespace SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands
{
    public class UpdatePaymentMethodCommand : UpdatePaymentMethodRequest, IRequest<bool>
    {
    }

    public class UpdatePaymentMethodHandler : IRequestHandler<UpdatePaymentMethodCommand, bool>
    {
        private readonly IPaymentMethodService _service;

        public UpdatePaymentMethodHandler(IPaymentMethodService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdatePaymentMethod(request);
        }
    }
}