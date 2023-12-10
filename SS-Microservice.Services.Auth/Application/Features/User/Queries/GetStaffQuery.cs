using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Infrastructure.Services.Address;
using SS_Microservice.Services.Auth.Infrastructure.Services.Address.Model.Request;

namespace SS_Microservice.Services.Auth.Application.Features.User.Queries
{
    public class GetStaffQuery : IRequest<StaffDto>
    {
        public long StaffId { get; set; }
    }

    public class GetStaffHandler : IRequestHandler<GetStaffQuery, StaffDto>
    {
        private readonly IUserService _userService;
        private readonly IAddressClientAPI _addressClientAPI;

        public GetStaffHandler(IUserService userService, IAddressClientAPI addressClientAPI)
        {
            _userService = userService;
            _addressClientAPI = addressClientAPI;
        }

        public async Task<StaffDto> Handle(GetStaffQuery request, CancellationToken cancellationToken)
        {
            var res = await _userService.GetStaff(request);
            var address = await _addressClientAPI.GetListAddressByUser(res.UserId, new GetListAddressRequest()
            {
                IsSortAscending = false,
                ColumnName = "Default"
            });

            if (address == null || address.Data == null)
            {
                throw new InternalServiceCommunicationException("Get address failed");
            }

            res.User.Addresses = address.Data.Items;

            return res;
        }
    }
}