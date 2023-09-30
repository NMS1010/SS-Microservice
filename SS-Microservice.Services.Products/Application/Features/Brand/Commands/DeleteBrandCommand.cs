using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Brand.Commands
{
    public class DeleteBrandCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }

    public class DeleteBrandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IBrandService _brandService;

        public DeleteBrandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            return await _brandService.DeleteBrand(request);
        }
    }
}