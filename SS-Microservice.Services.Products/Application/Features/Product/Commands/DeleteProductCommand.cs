using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductService _productService;

        public DeleteProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProduct(request);
        }
    }
}