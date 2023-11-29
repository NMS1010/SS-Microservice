using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Brand.Queries
{
    public class GetBrandQuery : IRequest<BrandDto>
    {
        public long Id { get; set; }
    }

    public class GetBrandHandler : IRequestHandler<GetBrandQuery, BrandDto>
    {
        private readonly IBrandService _brandService;

        public GetBrandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<BrandDto> Handle(GetBrandQuery request, CancellationToken cancellationToken)
        {
            return await _brandService.GetBrand(request);
        }
    }
}