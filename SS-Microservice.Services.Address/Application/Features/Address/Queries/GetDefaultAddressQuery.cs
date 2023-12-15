using MediatR;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Interfaces;

namespace SS_Microservice.Services.Address.Application.Features.Address.Queries
{
    public class GetDefaultAddressQuery : IRequest<AddressDto>
    {
        public string UserId { get; set; }
    }

    public class GetDefaultAddressHandler : IRequestHandler<GetDefaultAddressQuery, AddressDto>
    {
        private readonly IAddressService _addressService;

        public GetDefaultAddressHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<AddressDto> Handle(GetDefaultAddressQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetDefaultAddress(request);
        }
    }
}
