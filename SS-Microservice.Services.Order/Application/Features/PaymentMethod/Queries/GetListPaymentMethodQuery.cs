using MediatR;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.PaymentMethod;

namespace SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries
{
    public class GetListPaymentMethodQuery : GetPaymentMethodPagingRequest, IRequest<PaginatedResult<PaymentMethodDto>>
    {
    }

    public class GetAllPaymentMethodHandler : IRequestHandler<GetListPaymentMethodQuery, PaginatedResult<PaymentMethodDto>>
    {
        private readonly IPaymentMethodService _service;

        public GetAllPaymentMethodHandler(IPaymentMethodService service)
        {
            _service = service;
        }

        public async Task<PaginatedResult<PaymentMethodDto>> Handle(GetListPaymentMethodQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetListPaymentMethod(request);
        }
    }
}