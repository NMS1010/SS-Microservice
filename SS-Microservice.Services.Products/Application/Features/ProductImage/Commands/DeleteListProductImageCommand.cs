using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.ProductImage.Commands
{
    public class DeleteListProductImageCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }
    public class DeleteListProductImageHandler : IRequestHandler<DeleteListProductImageCommand, bool>
    {
        private readonly IProductImageService _productImageService;

        public DeleteListProductImageHandler(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<bool> Handle(DeleteListProductImageCommand request, CancellationToken cancellationToken)
        {
            return await _productImageService.DeleteListProductImage(request);
        }
    }
}
