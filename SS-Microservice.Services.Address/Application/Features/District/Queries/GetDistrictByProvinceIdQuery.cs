using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.Province.Queries;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Models.District;

namespace SS_Microservice.Services.Address.Application.Features.District.Queries
{
    public class GetDistrictByProvinceIdQuery : GetDistrictPagingRequest, IRequest<PaginatedResult<DistrictDto>>
    {
    }

    public class GetDistrictByProvinceIdHandler : IRequestHandler<GetDistrictByProvinceIdQuery, PaginatedResult<DistrictDto>>
    {
        private readonly IAddressService _addressService;

        public GetDistrictByProvinceIdHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<PaginatedResult<DistrictDto>> Handle(GetDistrictByProvinceIdQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetDistrictListByProvince(request);
        }
    }
}