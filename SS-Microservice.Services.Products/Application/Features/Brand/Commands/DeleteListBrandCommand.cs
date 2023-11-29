using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Brand.Commands
{
    public class DeleteListBrandCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }
    public class DeleteListBrandHandler : IRequestHandler<DeleteListBrandCommand, bool>
    {
        private readonly IBrandService _brandService;

        public DeleteListBrandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<bool> Handle(DeleteListBrandCommand request, CancellationToken cancellationToken)
        {
            return await _brandService.DeleteListBrand(request);
        }
    }
}
