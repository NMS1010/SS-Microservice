using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Commands
{
    public class DeleteListVariantCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }
    public class DeleteListVariantHandler : IRequestHandler<DeleteListVariantCommand, bool>
    {
        private readonly IVariantService _variantService;

        public DeleteListVariantHandler(IVariantService variantService)
        {
            _variantService = variantService;
        }

        public async Task<bool> Handle(DeleteListVariantCommand request, CancellationToken cancellationToken)
        {
            return await _variantService.DeleteListVariant(request);
        }
    }
}
