﻿using MediatR;
using SS_Microservice.Services.Auth.Application.Common.Interfaces;
using SS_Microservice.Services.Auth.Application.Features.User.Commands;

namespace SS_Microservice.Services.Auth.Application.Features.User.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UserUpdateCommand, bool>
    {
        private readonly IUserService _userService;

        public UpdateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUser(request);
        }
    }
}