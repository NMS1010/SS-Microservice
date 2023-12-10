using MediatR;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Brand;

namespace SS_Microservice.Services.Products.Application.Features.Brand.Queries
{
    public class GetListBrandQuery : GetBrandPagingRequest, IRequest<PaginatedResult<BrandDto>>
    {
    }

    public class GetListBrandHandler : IRequestHandler<GetListBrandQuery, PaginatedResult<BrandDto>>
    {
        private readonly IBrandService _brandService;

        public GetListBrandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<PaginatedResult<BrandDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            return await _brandService.GetListBrand(request);
        }
    }
}