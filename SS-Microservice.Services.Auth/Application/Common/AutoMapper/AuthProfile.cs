using AutoMapper;
using SS_Microservice.Services.Auth.Application.Features.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Model.Auth;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Application.Common.AutoMapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterUserCommand, AppUser>();

            CreateMap<LoginRequest, LoginQuery>();
            CreateMap<GoogleAuthRequest, GoogleAuthCommand>();
            CreateMap<VerifyOTPRequest, VerifyOTPCommand>();
            CreateMap<ResendOTPRequest, ResendOTPCommand>();
            CreateMap<ForgotPasswordRequest, ForgotPasswordCommand>();
            CreateMap<ResetPasswordRequest, ResetPasswordCommand>();
            CreateMap<RegisterRequest, RegisterUserCommand>();
            CreateMap<RefreshTokenRequest, RefreshTokenCommand>();
        }
    }
}
