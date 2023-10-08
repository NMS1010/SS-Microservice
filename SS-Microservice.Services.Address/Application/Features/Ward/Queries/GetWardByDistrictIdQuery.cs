using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Models.District;
using SS_Microservice.Services.Address.Application.Models.Ward;

namespace SS_Microservice.Services.Address.Application.Features.Ward.Queries
{
    public class GetWardByDistrictIdQuery : GetWardPagingRequest, IRequest<PaginatedResult<WardDto>>
    {
    }

    public class GetWardByProvinceIdHandler : IRequestHandler<GetWardByDistrictIdQuery, PaginatedResult<WardDto>>
    {
        private readonly IAddressService _addressService;

        public GetWardByProvinceIdHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<PaginatedResult<WardDto>> Handle(GetWardByDistrictIdQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetWardListByDistrict(request);
        }
    }
}