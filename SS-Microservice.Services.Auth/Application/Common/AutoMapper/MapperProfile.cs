using AutoMapper;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Features.User.Commands;
using SS_Microservice.Services.Auth.Application.Model.Auth;
using SS_Microservice.Services.Auth.Application.Model.User;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Application.Common.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LoginRequest, LoginQuery>();
            CreateMap<RegisterRequest, RegisterUserCommand>();
            CreateMap<RefreshTokenRequest, RefreshTokenCommand>();
            CreateMap<UserUpdateRequest, UserUpdateCommand>();
            CreateMap<AppUser, UserDto>();
        }
    }
}