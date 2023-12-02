using AutoMapper;
using SS_Microservice.Services.UserOperation.Application.Features.UserFollowProduct.Commands;
using SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct;

namespace SS_Microservice.Services.UserOperation.Application.Common.AutoMapper
{
    public class UserFollowProductProfile : Profile
    {
        public UserFollowProductProfile()
        {
            // mapping request - command
            CreateMap<FollowProductRequest, FollowProductCommand>();
            CreateMap<UnFollowProductRequest, UnFollowProductCommand>();
        }
    }
}
