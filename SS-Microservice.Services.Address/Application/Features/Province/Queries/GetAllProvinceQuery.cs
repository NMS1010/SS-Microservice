using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Models.Province;

namespace SS_Microservice.Services.Address.Application.Features.Province.Queries
{
    public class GetAllProvinceQuery : GetProvincePagingRequest, IRequest<PaginatedResult<ProvinceDto>>
    {
    }

    public class GetAllProvinceHandler : IRequestHandler<GetAllProvinceQuery, PaginatedResult<ProvinceDto>>
    {
        private readonly IAddressService _addressService;

        public GetAllProvinceHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<PaginatedResult<ProvinceDto>> Handle(GetAllProvinceQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetProvinceList(request);
        }
    }
}