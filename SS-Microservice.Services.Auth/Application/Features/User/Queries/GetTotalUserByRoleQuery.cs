using MediatR;
using Microsoft.AspNetCore.Identity;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Application.Features.User.Queries
{
    public class GetTotalUserByRoleQuery : IRequest<int>
    {
        public string Role { get; set; }
    }

    public class GetTotalUserByRoleHandler : IRequestHandler<GetTotalUserByRoleQuery, int>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetTotalUserByRoleHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int> Handle(GetTotalUserByRoleQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.GetUsersInRoleAsync(request.Role);

            return users.Count;
        }
    }
}
