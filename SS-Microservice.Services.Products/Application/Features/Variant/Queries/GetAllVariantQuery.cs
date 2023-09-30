using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Variant;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Queries
{
    public class GetAllVariantQuery : GetVariantPagingRequest, IRequest<PaginatedResult<VariantDto>>
    {
    }

    public class GetAllVariantHandler : IRequestHandler<GetAllVariantQuery, PaginatedResult<VariantDto>>
    {
        private readonly IProductVariantService _service;

        public GetAllVariantHandler(IProductVariantService service)
        {
            _service = service;
        }

        public async Task<PaginatedResult<VariantDto>> Handle(GetAllVariantQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllVariant(request);
        }
    }
}