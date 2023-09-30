using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries
{
    public class GetPaymentMethodByIdQuery : IRequest<PaymentMethodDto>
    {
        public long Id { get; set; }
    }

    public class GetPaymentMethodByIdHandler : IRequestHandler<GetPaymentMethodByIdQuery, PaymentMethodDto>
    {
        private readonly IPaymentMethodService _service;

        public GetPaymentMethodByIdHandler(IPaymentMethodService service)
        {
            _service = service;
        }

        public async Task<PaymentMethodDto> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaymentMethod(request);
        }
    }
}