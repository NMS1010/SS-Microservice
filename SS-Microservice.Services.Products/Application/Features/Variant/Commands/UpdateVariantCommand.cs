using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Variant;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Commands
{
    public class UpdateVariantCommand : UpdateVariantRequest, IRequest<bool>
    {
    }

    public class UpdateVariantHandler : IRequestHandler<UpdateVariantCommand, bool>
    {
        private readonly IVariantService _variantService;

        public UpdateVariantHandler(IVariantService variantService)
        {
            _variantService = variantService;
        }

        public async Task<bool> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
        {
            return await _variantService.UpdateVariant(request);
        }
    }
}