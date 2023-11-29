using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.ProductImage.Commands
{
    public class DeleteProductImageCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeleteProductImageHandler : IRequestHandler<DeleteProductImageCommand, bool>
    {
        private readonly IProductImageService _productImageService;

        public DeleteProductImageHandler(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<bool> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            return await _productImageService.DeleteProductImage(request);
        }
    }
}
