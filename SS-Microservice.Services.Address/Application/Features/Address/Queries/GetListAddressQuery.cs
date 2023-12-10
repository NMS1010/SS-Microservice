using MediatR;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Models.Address;

namespace SS_Microservice.Services.Address.Application.Features.Address.Queries
{
    public class GetListAddressQuery : GetAddressPagingRequest, IRequest<PaginatedResult<AddressDto>>
    {
    }

    public class GetListAddressHandler : IRequestHandler<GetListAddressQuery, PaginatedResult<AddressDto>>
    {
        private readonly IAddressService _addressService;

        public GetListAddressHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<PaginatedResult<AddressDto>> Handle(GetListAddressQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetListAddress(request);
        }
    }
}