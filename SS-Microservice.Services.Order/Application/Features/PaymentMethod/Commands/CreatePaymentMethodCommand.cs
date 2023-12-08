using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.PaymentMethod;

namespace SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands
{
    public class CreatePaymentMethodCommand : CreatePaymentMethodRequest, IRequest<long>
    {
    }

    public class CreatePaymentMethodHandler : IRequestHandler<CreatePaymentMethodCommand, long>
    {
        private readonly IPaymentMethodService _service;

        public CreatePaymentMethodHandler(IPaymentMethodService service)
        {
            _service = service;
        }

        public async Task<long> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreatePaymentMethod(request);
        }
    }
}