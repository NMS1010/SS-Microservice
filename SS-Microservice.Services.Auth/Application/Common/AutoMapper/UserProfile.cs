using AutoMapper;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.User.Commands;
using SS_Microservice.Services.Auth.Application.Features.User.Queries;
using SS_Microservice.Services.Auth.Application.Model.User;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Application.Common.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<Staff, StaffDto>();

            CreateMap<ChangePasswordRequest, ChangePasswordCommand>();

            CreateMap<CreateStaffRequest, CreateStaffCommand>();
            CreateMap<UpdateStaffRequest, UpdateStaffCommand>();
            CreateMap<UpdateUserRequest, UpdateUserCommand>();

            CreateMap<GetUserPagingRequest, GetListStaffQuery>();
            CreateMap<GetUserPagingRequest, GetListUserQuery>();

            CreateMap<CreateStaffCommand, AppUser>();
            CreateMap<UpdateUserCommand, AppUser>();
        }
    }
}
