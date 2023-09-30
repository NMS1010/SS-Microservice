using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Brand.Commands;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Brand;

namespace SS_Microservice.Services.Products.Application.Features.Brand.Queries
{
    public class GetAllBrandQuery : GetBrandPagingRequest, IRequest<PaginatedResult<BrandDto>>
    {
    }

    public class GetAllBrandHandler : IRequestHandler<GetAllBrandQuery, PaginatedResult<BrandDto>>
    {
        private readonly IBrandService _brandService;

        public GetAllBrandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<PaginatedResult<BrandDto>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            return await _brandService.GetAllBrand(request);
        }
    }
}