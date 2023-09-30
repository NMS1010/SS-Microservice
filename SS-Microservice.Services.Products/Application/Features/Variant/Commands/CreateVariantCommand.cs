using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Variant;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Commands
{
    public class CreateVariantCommand : CreateVariantRequest, IRequest<bool>
    {
    }

    public class CreateVariantHandler : IRequestHandler<CreateVariantCommand, bool>
    {
        private readonly IProductVariantService _service;

        public CreateVariantHandler(IProductVariantService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreateVariantCommand request, CancellationToken cancellationToken)
        {
            return await _service.AddVariant(request);
        }
    }
}