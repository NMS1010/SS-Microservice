using MediatR;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Interfaces;

namespace SS_Microservice.Services.Address.Application.Features.District.Queries
{
    public class GetListDistrictByProvinceIdQuery : IRequest<List<DistrictDto>>
    {
        public long ProvinceId { get; set; }
    }

    public class GetListDistrictByProvinceIdHandler : IRequestHandler<GetListDistrictByProvinceIdQuery, List<DistrictDto>>
    {
        private readonly IAddressService _addressService;

        public GetListDistrictByProvinceIdHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<List<DistrictDto>> Handle(GetListDistrictByProvinceIdQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetListDistrictByProvince(request);
        }
    }
}