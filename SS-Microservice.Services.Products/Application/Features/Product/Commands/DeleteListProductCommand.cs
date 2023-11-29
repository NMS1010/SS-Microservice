using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class DeleteListProductCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }
    public class DeleteListProductHandler : IRequestHandler<DeleteListProductCommand, bool>
    {
        private readonly IProductService _brandService;

        public DeleteListProductHandler(IProductService brandService)
        {
            _brandService = brandService;
        }

        public async Task<bool> Handle(DeleteListProductCommand request, CancellationToken cancellationToken)
        {
            return await _brandService.DeleteListProduct(request);
        }
    }
}
