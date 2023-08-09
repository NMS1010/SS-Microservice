using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Auth.Application.Common.Interfaces;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.User.Commands;
using SS_Microservice.Services.Auth.Application.User.Queries;
using SS_Microservice.Services.Auth.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SS_Microservice.Services.Auth.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUser(GetUserQuery query)
        {
            var user = await _userManager.FindByIdAsync(query.UserId) ?? throw new NotFoundException("Cannot find this user");
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> UpdateUser(UserUpdateCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.UserId) ?? throw new NotFoundException("Cannot find this user");

            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded)
            {
                throw new Exception("Cannot update this user");
            }
            return true;
        }
    }
}