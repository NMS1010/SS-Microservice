using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries
{
    public class GetPaymentMethodQuery : IRequest<PaymentMethodDto>
    {
        public long Id { get; set; }
    }

    public class GetPaymentMethodHandler : IRequestHandler<GetPaymentMethodQuery, PaymentMethodDto>
    {
        private readonly IPaymentMethodService _service;

        public GetPaymentMethodHandler(IPaymentMethodService service)
        {
            _service = service;
        }

        public async Task<PaymentMethodDto> Handle(GetPaymentMethodQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaymentMethod(request);
        }
    }
}