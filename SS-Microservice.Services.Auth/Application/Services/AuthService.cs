using AutoMapper;
using Google.Apis.Auth;
using green_craze_be_v1.Application.Specification.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Auth.Application.Common.Constants;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Domain.Entities;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace SS_Microservice.Services.Auth.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IJwtService jwtService,
            IMapper mapper,
            ICurrentUserService currentUserService,
            ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        private async Task<AuthDto> GenerateAuthCredential(AppUser user)
        {
            string accessToken = await _jwtService.CreateJWT(user.Id);
            string refreshToken = _jwtService.CreateRefreshToken();
            DateTime refreshTokenExpiredTime = DateTime.Now.AddDays(TOKEN_TYPE.REFRESH_TOKEN_EXPIRY_DAYS);

            var u = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(new UserSpecification(user.Id))
                ?? throw new NotFoundException("Cannot find current user");

            var userToken = u.AppUserTokens.FirstOrDefault(x => x.Type == TOKEN_TYPE.REFRESH_TOKEN);
            if (userToken == null)
            {
                u.AppUserTokens.Add(new AppUserToken()
                {
                    Token = refreshToken,
                    ExpiredAt = refreshTokenExpiredTime,
                    Type = TOKEN_TYPE.REFRESH_TOKEN,
                    CreatedAt = DateTime.Now,
                    CreatedBy = _currentUserService.UserId
                });
            }
            else
            {
                userToken.Token = refreshToken;
                userToken.ExpiredAt = refreshTokenExpiredTime;
                userToken.UpdatedAt = DateTime.Now;
                userToken.UpdatedBy = _currentUserService.UserId;
            }

            var isSuccess = await _userManager.UpdateAsync(u);
            if (!isSuccess.Succeeded)
                throw new Exception("Cannot login, please contact administrator");

            return new AuthDto { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<AuthDto> Authenticate(LoginQuery query)
        {
            var user = await _userManager.FindByEmailAsync(query.Email)
                    ?? throw new InvalidRequestException("Email is incorrect, cannot login");
            var res = await _signInManager.CheckPasswordSignInAsync(user, query.Password, lockoutOnFailure: true);
            if (res.IsLockedOut)
            {
                throw new AccessDeniedException("Your account has been lockout, unlock in " + user.LockoutEnd);
            }
            if (!res.Succeeded)
                throw new InvalidRequestException("Password is incorrect");

            if (user.Status == USER_STATUS.IN_ACTIVE)
                throw new AccessDeniedException("Your account has been banned");

            if (!user.EmailConfirmed)
            {
                await ResendOTP(new ResendOTPCommand()
                {
                    Email = user.Email,
                    Type = TOKEN_TYPE.REGISTER_OTP
                });
                throw new AccessDeniedException("Your account hasn't been confirmed");
            }

            return await GenerateAuthCredential(user);
        }

        private static async Task<GoogleJsonWebSignature.Payload> IsGoogleTokenValid(string token)
        {
            try
            {
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(token);
                return payload;
            }
            catch
            {
                throw new InvalidRequestException("Google token is invalid, cannot login");
            }
        }

        public async Task<AuthDto> AuthenticateWithGoogle(GoogleAuthCommand command)
        {
            var payload = await IsGoogleTokenValid(command.GoogleToken);
            var user = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(new UserSpecification(payload.Email, true));

            if (user != null)
            {
                if (user.Status == USER_STATUS.IN_ACTIVE)
                    throw new AccessDeniedException("Your account has been banned");
                user.EmailConfirmed = true;
                var userOTP = user.AppUserTokens.FirstOrDefault(x => x.Type == TOKEN_TYPE.REGISTER_OTP);
                if (userOTP != null)
                {
                    userOTP.Token = null;
                    userOTP.ExpiredAt = null;
                    userOTP.UpdatedAt = DateTime.Now;
                    userOTP.UpdatedBy = _currentUserService.UserId;
                }
                return await GenerateAuthCredential(user);
            }
            var registerCommand = new RegisterUserCommand()
            {
                Email = payload.Email,
                FirstName = payload.FamilyName,
                LastName = payload.GivenName,
                Password = payload.Subject
            };
            var userDto = await Register(registerCommand, isGoogleAuthen: true);
            user = await _userManager.FindByIdAsync(user.Id);
            user.Avatar = payload.Picture;

            var resp = await GenerateAuthCredential(user);

            return resp;
        }

        public async Task<AuthDto> RefreshToken(RefreshTokenCommand command)
        {
            var userPrincipal = _jwtService.ValidateExpiredJWT(command.AccessToken)
                ?? throw new UnAuthorizedException("Invalid access token");
            var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(new UserSpecification(userId))
                ?? throw new NotFoundException("Cannot find current user");

            var userToken = user.AppUserTokens.FirstOrDefault(x => x.Type == TOKEN_TYPE.REFRESH_TOKEN);
            if (userToken is null || userToken.Token != command.RefreshToken)
            {
                throw new SecurityTokenValidationException("Invalid refresh token");
            }
            if (userToken.ExpiredAt <= DateTime.Now)
            {
                throw new SecurityTokenValidationException("Refresh token has expired");
            }
            var newAccessToken = await _jwtService.CreateJWT(user.Id);
            var newRefreshToken = _jwtService.CreateRefreshToken();
            userToken.Token = newRefreshToken;
            userToken.ExpiredAt = DateTime.Now.AddDays(TOKEN_TYPE.REFRESH_TOKEN_EXPIRY_DAYS);
            userToken.UpdatedAt = DateTime.Now;
            userToken.UpdatedBy = _currentUserService.UserId;
            await _userManager.UpdateAsync(user);

            return new AuthDto { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        }

        public async Task<CreateUserOTPDto> Register(RegisterUserCommand command, bool isGoogleAuthen = false)
        {
            var user = _mapper.Map<AppUser>(command);
            user.Status = USER_STATUS.ACTIVE;
            user.UserName = Regex.Replace(command.Email, "[^A-Za-z0-9 -]", "");
            user.CreatedAt = DateTime.Now;
            user.CreatedBy = "System";
            if (isGoogleAuthen)
            {
                user.EmailConfirmed = true;
            }
            var res = await _userManager.CreateAsync(user, command.Password);

            if (res.Succeeded)
            {
                List<string> roles = new()
                {
                    USER_ROLE.USER
                };
                await _userManager.AddToRolesAsync(user, roles);

                CreateUserOTPDto resp = new()
                {
                    Email = user.Email,
                    Name = user.FirstName + " " + user.LastName,
                    UserId = user.Id
                };
                if (!isGoogleAuthen)
                {
                    var otp = _tokenService.GenerateOTP();
                    user.AppUserTokens.Add(new AppUserToken()
                    {
                        Token = otp,
                        ExpiredAt = DateTime.Now.AddMinutes(TOKEN_TYPE.OTP_EXPIRY_MINUTES),
                        Type = TOKEN_TYPE.REGISTER_OTP,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "System"
                    });

                    await _userManager.UpdateAsync(user);
                    resp.OTP = otp;
                }

                return resp;
            }

            string error = "";
            res.Errors.ToList().ForEach(x => error += x.Description + "/n");
            throw new Exception(error);
        }

        public async Task RevokeAllRefreshToken()
        {
            var users = await _unitOfWork.Repository<AppUser>().ListAsync(new UserSpecification());
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

        public async Task RevokeRefreshToken(RevokeRefreshTokenCommand command)
        {
            var user = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(new UserSpecification(command.UserId))
                ?? throw new NotFoundException("Cannot find current user");
            var userToken = user.AppUserTokens.FirstOrDefault(x => x.Type == TOKEN_TYPE.REFRESH_TOKEN)
                ?? throw new NotFoundException("Refresh token of current user is not found");
            userToken.Token = null;
            userToken.ExpiredAt = null;
            userToken.UpdatedAt = DateTime.Now;
            userToken.UpdatedBy = _currentUserService.UserId;
            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> VerifyOTP(VerifyOTPCommand command)
        {
            var user = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(new UserSpecification(command.Email, true))
                ?? throw new NotFoundException("Cannot find user with email: " + command.Email);

            var userToken = user.AppUserTokens.FirstOrDefault(x => x.Type == command.Type) ??
                throw new InvalidRequestException("Cannot verify, you must follow the steps in order");

            if (userToken.Token != command.OTP)
            {
                throw new InvalidRequestException("OTP is invalid");
            }
            if (userToken.ExpiredAt <= DateTime.Now)
            {
                throw new InvalidRequestException("OTP is expired");
            }

            userToken.Token = null;
            userToken.ExpiredAt = null;
            if (command.Type == TOKEN_TYPE.REGISTER_OTP)
            {
                user.EmailConfirmed = true;
                user.UpdatedAt = DateTime.Now;
                user.UpdatedBy = _currentUserService.UserId;
            }

            userToken.UpdatedAt = DateTime.Now;
            userToken.UpdatedBy = _currentUserService.UserId;

            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<CreateUserOTPDto> ResendOTP(ResendOTPCommand command)
        {
            var user = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(new UserSpecification(command.Email, true))
                ?? throw new NotFoundException("Cannot find user with email: " + command.Email);
            var userToken = user.AppUserTokens.FirstOrDefault(x => x.Type == command.Type);

            if (userToken == null && command.Type == TOKEN_TYPE.REGISTER_OTP)
                throw new InvalidRequestException("This user hasn't been signed up before, invalid command");

            if (userToken == null && command.Type == TOKEN_TYPE.FORGOT_PASSWORD_OTP)
                throw new InvalidRequestException("You need to perform forgot password feature for your account before, invalid command");

            if (command.Type == TOKEN_TYPE.REGISTER_OTP && user.EmailConfirmed)
                throw new InvalidRequestException("Your account has been verified, invalid command");

            var otp = _tokenService.GenerateOTP();

            userToken.Token = otp;
            userToken.ExpiredAt = DateTime.Now.AddMinutes(TOKEN_TYPE.OTP_EXPIRY_MINUTES);
            userToken.UpdatedAt = DateTime.Now;
            userToken.UpdatedBy = _currentUserService.UserId;

            await _userManager.UpdateAsync(user);


            return new CreateUserOTPDto()
            {
                Email = user.Email,
                Name = user.FirstName + " " + user.LastName,
                OTP = otp
            };
        }

        public async Task<CreateUserOTPDto> ForgotPassword(ForgotPasswordCommand command)
        {
            var user = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(new UserSpecification(command.Email, true))
                ?? throw new NotFoundException("Cannot find user with email: " + command.Email);
            if (!user.EmailConfirmed)
                throw new InvalidRequestException("Your account hasn't been confirmed, please verify it before");

            var userToken = user.AppUserTokens.FirstOrDefault(x => x.Type == TOKEN_TYPE.FORGOT_PASSWORD_OTP);

            var otp = _tokenService.GenerateOTP();
            if (userToken is null)
            {
                user.AppUserTokens.Add(new AppUserToken()
                {
                    Token = otp,
                    ExpiredAt = DateTime.Now.AddMinutes(TOKEN_TYPE.OTP_EXPIRY_MINUTES),
                    Type = TOKEN_TYPE.FORGOT_PASSWORD_OTP,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System"
                });
            }
            else
            {
                userToken.Token = otp;
                userToken.ExpiredAt = DateTime.Now.AddMinutes(TOKEN_TYPE.OTP_EXPIRY_MINUTES);
                userToken.UpdatedAt = DateTime.Now;
                userToken.UpdatedBy = _currentUserService.UserId;
            }
            await _userManager.UpdateAsync(user);

            return new CreateUserOTPDto()
            {
                Email = user.Email,
                Name = user.FirstName + " " + user.LastName,
                OTP = otp
            };
        }

        public async Task<bool> ResetPassword(ResetPasswordCommand command)
        {
            await VerifyOTP(new VerifyOTPCommand()
            {
                Email = command.Email,
                OTP = command.OTP,
                Type = TOKEN_TYPE.FORGOT_PASSWORD_OTP
            });

            var user = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(new UserSpecification(command.Email, true))
                ?? throw new NotFoundException("Cannot find user with email: " + command.Email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var res = await _userManager.ResetPasswordAsync(user, token, command.Password);
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = _currentUserService.UserId;
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            if (!res.Succeeded)
                throw new Exception("Cannot reset your password, please contact administrator");

            return res.Succeeded;
        }

    }
}