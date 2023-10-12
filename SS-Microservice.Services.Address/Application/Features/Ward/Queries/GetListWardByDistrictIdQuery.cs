using MediatR;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Interfaces;

namespace SS_Microservice.Services.Address.Application.Features.Ward.Queries
{
    public class GetListWardByDistrictIdQuery : IRequest<List<WardDto>>
    {
        public long DistrictId { get; set; }
    }

    public class GetListWardByDistrictIdHandler : IRequestHandler<GetListWardByDistrictIdQuery, List<WardDto>>
    {
        private readonly IAddressService _addressService;

        public GetListWardByDistrictIdHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<List<WardDto>> Handle(GetListWardByDistrictIdQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetListWardByDistrict(request);
        }
    }
}