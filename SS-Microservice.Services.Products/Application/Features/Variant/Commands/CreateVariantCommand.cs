using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Variant;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Commands
{
    public class CreateVariantCommand : CreateVariantRequest, IRequest<long>
    {
    }

    public class CreateVariantHandler : IRequestHandler<CreateVariantCommand, long>
    {
        private readonly IVariantService _variantService;

        public CreateVariantHandler(IVariantService variantService)
        {
            _variantService = variantService;
        }

        public async Task<long> Handle(CreateVariantCommand request, CancellationToken cancellationToken)
        {
            return await _variantService.CreateVariant(request);
        }
    }
}