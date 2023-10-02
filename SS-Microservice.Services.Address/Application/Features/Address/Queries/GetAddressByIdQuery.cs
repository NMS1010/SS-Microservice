using MediatR;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.Address.Commands;
using SS_Microservice.Services.Address.Application.Interfaces;

namespace SS_Microservice.Services.Address.Application.Features.Address.Queries
{
    public class GetAddressByIdQuery : IRequest<AddressDto>
    {
        public long Id { get; set; }
        public string UserId { get; set; }
    }

    public class GetAddressByIdHandler : IRequestHandler<GetAddressByIdQuery, AddressDto>
    {
        private readonly IAddressService _addressService;

        public GetAddressByIdHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<AddressDto> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetAddress(request);
        }
    }
}