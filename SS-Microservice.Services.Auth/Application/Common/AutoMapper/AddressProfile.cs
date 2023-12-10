using AutoMapper;
using SS_Microservice.Services.Auth.Application.Features.Address.Commands;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Common.AutoMapper
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressRequest, CreateAddressCommand>();
            CreateMap<UpdateAddressRequest, UpdateAddressCommand>();
        }
    }
}
