using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Brand;

namespace SS_Microservice.Services.Products.Application.Features.Brand.Commands
{
    public class UpdateBrandCommand : UpdateBrandRequest, IRequest<bool>
    {
    }

    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IBrandService _brandService;

        public UpdateBrandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            return await _brandService.UpdateBrand(request);
        }
    }
}