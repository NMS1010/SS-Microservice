using MediatR;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Interfaces;

namespace SS_Microservice.Services.Address.Application.Features.Ward.Queries
{
    public class GetListWardByDistrictQuery : IRequest<List<WardDto>>
    {
        public long DistrictId { get; set; }
    }

    public class GetListWardByDistrictHandler : IRequestHandler<GetListWardByDistrictQuery, List<WardDto>>
    {
        private readonly IAddressService _addressService;

        public GetListWardByDistrictHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<List<WardDto>> Handle(GetListWardByDistrictQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetListWardByDistrict(request);
        }
    }
}