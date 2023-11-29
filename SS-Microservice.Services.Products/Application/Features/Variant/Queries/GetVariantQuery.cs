using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Queries
{
    public class GetVariantQuery : IRequest<VariantDto>
    {
        public long Id { get; set; }
    }

    public class GetVariantHandler : IRequestHandler<GetVariantQuery, VariantDto>
    {
        private readonly IVariantService _variantService;

        public GetVariantHandler(IVariantService variantService)
        {
            _variantService = variantService;
        }

        public async Task<VariantDto> Handle(GetVariantQuery request, CancellationToken cancellationToken)
        {
            return await _variantService.GetVariant(request);
        }
    }
}