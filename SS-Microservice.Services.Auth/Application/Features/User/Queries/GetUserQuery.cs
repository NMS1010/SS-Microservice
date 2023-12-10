using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Infrastructure.Services.Address;
using SS_Microservice.Services.Auth.Infrastructure.Services.Address.Model.Request;

namespace SS_Microservice.Services.Auth.Application.Features.User.Queries
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public string UserId { get; set; }
    }

    public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserService _userService;
        private readonly IAddressClientAPI _addressClientAPI;

        public GetUserHandler(IUserService userService, IAddressClientAPI addressClientAPI)
        {
            _userService = userService;
            _addressClientAPI = addressClientAPI;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var res = await _userService.GetUser(request);

            var address = await _addressClientAPI.GetListAddressByUser(res.Id, new GetListAddressRequest()
            {
                IsSortAscending = false,
                ColumnName = "Default"
            });

            if (address == null || address.Data == null)
            {
                throw new InternalServiceCommunicationException("Get address failed");
            }

            res.Addresses = address.Data.Items;

            return res;
        }
    }
}