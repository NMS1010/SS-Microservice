using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Commands
{
    public class DeleteVariantCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeleteVariantHandler : IRequestHandler<DeleteVariantCommand, bool>
    {
        private readonly IVariantService _variantService;

        public DeleteVariantHandler(IVariantService variantService)
        {
            _variantService = variantService;
        }

        public async Task<bool> Handle(DeleteVariantCommand request, CancellationToken cancellationToken)
        {
            return await _variantService.DeleteVariant(request);
        }
    }
}