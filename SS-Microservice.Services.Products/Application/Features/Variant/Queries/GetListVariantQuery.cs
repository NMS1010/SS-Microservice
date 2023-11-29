using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Variant;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Queries
{
    public class GetListVariantQuery : GetVariantPagingRequest, IRequest<PaginatedResult<VariantDto>>
    {
    }

    public class GetListVariantHandler : IRequestHandler<GetListVariantQuery, PaginatedResult<VariantDto>>
    {
        private readonly IVariantService _variantService;

        public GetListVariantHandler(IVariantService variantService)
        {
            _variantService = variantService;
        }

        public async Task<PaginatedResult<VariantDto>> Handle(GetListVariantQuery request, CancellationToken cancellationToken)
        {
            return await _variantService.GetListVariant(request);
        }
    }
}