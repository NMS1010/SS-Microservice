using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Auth.Application.Common.Constants;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;
using SS_Microservice.Services.Auth.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SS_Microservice.Services.Auth.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AuthService(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IJwtService jwtService,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<AuthDto> Authenticate(LoginQuery request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                    ?? throw new ValidationException("Email is incorrect, cannot login");
            var res = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
            if (res.IsLockedOut)
            {
                throw new AccessDeniedException("Your account has been lockout, unlock in " + user.LockoutEnd);
            }
            if (!res.Succeeded)
                throw new ValidationException("Password is incorrect");
            if (user.Status == USER_STATUS.IN_ACTIVE)
                throw new AccessDeniedException("Your account has been banned");
            //if (!user.EmailConfirmed)
            //    throw new ForbiddenAccessException("Your account hasn't been confirmed");

            string accessToken = await _jwtService.CreateJWT(user.Id);
            string refreshToken = _jwtService.CreateRefreshToken();
            DateTime refreshTokenExpiredTime = DateTime.Now.AddDays(7);
            user.AppUserTokens.Add(new AppUserToken()
            {
                Token = refreshToken,
                ExpiredAt = refreshTokenExpiredTime,
                Type = TOKEN_TYPE.REFRESH_TOKEN,
                CreatedAt = DateTime.Now,
                CreatedBy = "System"
            });

            var isSuccess = await _userManager.UpdateAsync(user);
            if (!isSuccess.Succeeded)
                throw new Exception("Cannot login, please contact administrator");
            return new AuthDto { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<AuthDto> RefreshToken(RefreshTokenCommand request)
        {
            var userPrincipal = _jwtService.ValidateExpiredJWT(request.AccessToken)
                ?? throw new UnauthorizedException("Invalid access token");
            var userName = userPrincipal.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var userToken = user.AppUserTokens.FirstOrDefault(x => x.Type == TOKEN_TYPE.REFRESH_TOKEN);
            if (user is null || userToken is null || userToken.Token != request.RefreshToken || userToken.ExpiredAt <= DateTime.Now)
            {
                throw new UnauthorizedException("Invalid access token or refresh token");
            }
            var newAccessToken = await _jwtService.CreateJWT(user.Id);
            var newRefreshToken = _jwtService.CreateRefreshToken();
            userToken.Token = newRefreshToken;
            userToken.UpdatedAt = DateTime.Now;
            userToken.UpdatedBy = _currentUserService.UserId;
            await _userManager.UpdateAsync(user);

            return new AuthDto { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        }

        public async Task<string> Register(RegisterUserCommand request)
        {
            var user = _mapper.Map<AppUser>(request);
            user.Status = USER_STATUS.ACTIVE;
            user.UserName = Regex.Replace(request.Email, "[^A-Za-z0-9 -]", "");
            user.CreatedAt = DateTime.Now;
            user.CreatedBy = "System";
            var res = await _userManager.CreateAsync(user, request.Password);

            if (res.Succeeded)
            {
                List<string> roles = new()
                    {
                       USER_ROLE.USER
                    };
                await _userManager.AddToRolesAsync(user, roles);
                //if (!string.IsNullOrEmpty(request.LoginProvider))
                //{
                //    await _userManager.AddLoginAsync(user, new UserLoginInfo(request.LoginProvider, request.ProviderKey, request.LoginProvider));
                //}
                //if (!string.IsNullOrEmpty(request.Host))
                //{
                //    bool isSend = await SendConfirmToken(user, request.Host);
                //    if (!isSend)
                //    {
                //        throw new Exception("Cannot send mail");
                //    }
                //}
                return user.Id;
            }

            string error = "";
            res.Errors.ToList().ForEach(x => error += (x.Description + "/n"));
            throw new Exception(error);
        }

        public async Task RevokeAllRefreshToken()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userToken = user.AppUserTokens.FirstOrDefault(x => x.Type == TOKEN_TYPE.REFRESH_TOKEN)
                    ?? throw new NotFoundException("Refresh token of user is not found");
                userToken.Token = null;
                userToken.ExpiredAt = null;
                userToken.UpdatedAt = DateTime.Now;
                userToken.UpdatedBy = _currentUserService.UserId;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<bool> RevokeRefreshToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NotFoundException("User is not found");
            var userToken = user.AppUserTokens.FirstOrDefault(x => x.Type == TOKEN_TYPE.REFRESH_TOKEN)
                ?? throw new NotFoundException("Refresh token of user is not found");
            userToken.Token = null;
            userToken.ExpiredAt = null;
            userToken.UpdatedAt = DateTime.Now;
            userToken.UpdatedBy = _currentUserService.UserId;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }
    }
}