using AutoMapper;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Application.Common.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<AppRole, RoleDto>();
        }
    }
}
