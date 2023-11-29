using MediatR;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Interfaces;

namespace SS_Microservice.Services.Address.Application.Features.District.Queries
{
    public class GetListDistrictByProvinceQuery : IRequest<List<DistrictDto>>
    {
        public long ProvinceId { get; set; }
    }

    public class GetListDistrictByProvinceHandler : IRequestHandler<GetListDistrictByProvinceQuery, List<DistrictDto>>
    {
        private readonly IAddressService _addressService;

        public GetListDistrictByProvinceHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<List<DistrictDto>> Handle(GetListDistrictByProvinceQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetListDistrictByProvince(request);
        }
    }
}