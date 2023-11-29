using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Queries
{
    public class GetListVariantByProductQuery : IRequest<List<VariantDto>>
    {
        public long ProductId { get; set; }
    }

    public class GetListVariantByProductHandler : IRequestHandler<GetListVariantByProductQuery, List<VariantDto>>
    {
        private readonly IVariantService _variantService;

        public GetListVariantByProductHandler(IVariantService variantService)
        {
            _variantService = variantService;
        }

        public async Task<List<VariantDto>> Handle(GetListVariantByProductQuery request, CancellationToken cancellationToken)
        {
            return await _variantService.GetListVariantByProductId(request);
        }
    }
}
