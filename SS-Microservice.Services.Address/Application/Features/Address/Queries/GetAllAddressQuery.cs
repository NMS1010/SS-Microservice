using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Models.Address;

namespace SS_Microservice.Services.Address.Application.Features.Address.Queries
{
    public class GetAllAddressQuery : GetAddressPagingRequest, IRequest<PaginatedResult<AddressDto>>
    {
        public string UserId { get; set; }
    }

    public class GetAllAddressHandler : IRequestHandler<GetAllAddressQuery, PaginatedResult<AddressDto>>
    {
        private readonly IAddressService _addressService;

        public GetAllAddressHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<PaginatedResult<AddressDto>> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
        {
            return await _addressService.GetAddressList(request);
        }
    }
}