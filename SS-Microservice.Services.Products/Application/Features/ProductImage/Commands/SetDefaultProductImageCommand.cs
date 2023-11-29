using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.ProductImage.Commands
{
    public class SetDefaultProductImageCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
    }

    public class SetDefaultProductImageHandler : IRequestHandler<SetDefaultProductImageCommand, bool>
    {
        private readonly IProductImageService _productImageService;

        public SetDefaultProductImageHandler(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<bool> Handle(SetDefaultProductImageCommand request, CancellationToken cancellationToken)
        {
            return await _productImageService.SetDefaultProductImage(request);
        }
    }
}
