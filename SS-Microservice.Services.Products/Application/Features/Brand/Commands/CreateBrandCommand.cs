using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Brand;

namespace SS_Microservice.Services.Products.Application.Features.Brand.Commands
{
    public class CreateBrandCommand : CreateBrandRequest, IRequest<long>
    {
    }

    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, long>
    {
        private readonly IBrandService _brandService;

        public CreateBrandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<long> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            return await _brandService.CreateBrand(request);
        }
    }
}