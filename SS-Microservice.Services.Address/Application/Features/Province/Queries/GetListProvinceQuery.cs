using MediatR;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Interfaces;

namespace SS_Microservice.Services.Address.Application.Features.Province.Queries
{
    public class GetListProvinceQuery : IRequest<List<ProvinceDto>>
    {
    }

    public class GetListProvinceHandler : IRequestHandler<GetListProvinceQuery, List<ProvinceDto>>
    {
        private readonly IAddressService _addressService;

        public GetListProvinceHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<List<ProvinceDto>> Handle(GetListProvinceQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetListProvince();
        }
    }
}