using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SS_Microservice.Services.Auth.Application.Common.Constants;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Auth.Application.Features.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Authenticate(LoginQuery request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                    throw new NotFoundException("Username/password is incorrect");
                var res = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: true);
                if (res.IsLockedOut)
                {
                    throw new ForbiddenAccessException("Your account has been lockout, unlock in " + user.LockoutEnd);
                }
                if (!res.Succeeded)
                    throw new NotFoundException("Username/password is incorrect");
                if (user.Status == USER_STATUS.IN_ACTIVE)
                    throw new ForbiddenAccessException("Your account has been banned");
                //if (!user.EmailConfirmed)
                //    throw new ForbiddenAccessException("Your account hasn't been confirmed");

                string accessToken = await _jwtService.CreateJWT(user.Id);
                string refreshToken = _jwtService.CreateRefreshToken();
                DateTime refreshTokenExpiredTime = DateTime.Now.AddDays(7);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiredTime = refreshTokenExpiredTime;
                var isSuccess = await _userManager.UpdateAsync(user);
                if (!isSuccess.Succeeded)
                    throw new Exception("Cannot login, please contact administrator");
                return new AuthResponse { AccessToken = accessToken, RefreshToken = refreshToken };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AuthResponse> RefreshToken(RefreshTokenCommand request)
        {
            var userPrincipal = _jwtService.ValidateExpiredJWT(request.AccessToken);
            if (userPrincipal is null)
            {
                throw new UnauthorizedException("Invalid access token");
            }
            var userName = userPrincipal.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiredTime <= DateTime.Now)
            {
                throw new UnauthorizedException("Invalid access token or refresh token");
            }
            var newAccessToken = await _jwtService.CreateJWT(user.Id);
            var newRefreshToken = _jwtService.CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new AuthResponse { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        }

        public async Task<string> Register(RegisterUserCommand request)
        {
            try
            {
                var user = new AppUser()
                {
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.Username,
                    Status = USER_STATUS.ACTIVE,
                    Avatar = "default-user.png"
                };

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
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task RevokeAllToken()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiredTime = null;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task RevokeToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            user.RefreshToken = null;
            user.RefreshTokenExpiredTime = null;
            await _userManager.UpdateAsync(user);
        }
    }
}