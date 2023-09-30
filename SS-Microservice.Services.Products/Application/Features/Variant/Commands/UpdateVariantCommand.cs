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
        private readonly IProductVariantService _service;

        public UpdateVariantHandler(IProductVariantService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateVariant(request);
        }
    }
}