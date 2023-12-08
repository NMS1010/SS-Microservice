using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SS_Microservice.Common.Services.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";

        public bool IsInRole(string role)
        {
            var userRoles = _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role);

            return userRoles.FirstOrDefault(x => x.Value == role) != null;
        }

    }
}