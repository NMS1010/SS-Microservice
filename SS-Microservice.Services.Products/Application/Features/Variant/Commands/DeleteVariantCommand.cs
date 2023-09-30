using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Commands
{
    public class DeleteVariantCommand : IRequest<bool>
    {
        public string ProductId { get; set; }
        public string VariantId { get; set; }
    }

    public class DeleteVariantHandler : IRequestHandler<DeleteVariantCommand, bool>
    {
        private readonly IProductVariantService _service;

        public DeleteVariantHandler(IProductVariantService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteVariantCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteVariant(request);
        }
    }
}