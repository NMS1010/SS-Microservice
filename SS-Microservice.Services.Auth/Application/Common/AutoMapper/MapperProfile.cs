using AutoMapper;
using SS_Microservice.Services.Auth.Application.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Model;
using SS_Microservice.Services.Auth.Core.Entities;

namespace SS_Microservice.Services.Auth.Application.Common.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LoginRequest, LoginQuery>();
            CreateMap<RegisterRequest, RegisterCommand>();
            CreateMap<RefreshTokenRequest, RefreshTokenCommand>();
        }
    }
}