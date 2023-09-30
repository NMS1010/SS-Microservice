using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class DeleteProductImageCommand : IRequest<bool>
    {
        public string ProductId { get; set; }
        public string ProductImageId { get; set; }
    }

    public class DeleteProductImageHandler : IRequestHandler<DeleteProductImageCommand, bool>
    {
        private readonly IProductService _productService;

        public DeleteProductImageHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProductImage(request);
        }
    }
}