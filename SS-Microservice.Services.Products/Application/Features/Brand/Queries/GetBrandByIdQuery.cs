using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Brand.Queries
{
    public class GetBrandByIdQuery : IRequest<BrandDto>
    {
        public string Id { get; set; }
    }

    public class GetBrandByIdHandler : IRequestHandler<GetBrandByIdQuery, BrandDto>
    {
        private readonly IBrandService _brandService;

        public GetBrandByIdHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            return await _brandService.GetBrandById(request);
        }
    }
}