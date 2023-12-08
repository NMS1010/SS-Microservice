using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SS_Microservice.Common.Configuration;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SS_Microservice.Services.Infrastructure.Application.Common.SignalR
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InfrastructureHub : Hub
    {
        private readonly IConfiguration _configuration;

        public InfrastructureHub(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static Dictionary<string, int> clientsNotification = new();



        private ClaimsPrincipal ValidateExpiredJWT(string token)
        {
            IdentityModelEventSource.ShowPII = true;

            var jwtOptions = _configuration.GetOptions<JwtConfigOptions>("Tokens");
            TokenValidationParameters validationParameters = new()
            {
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidAudience = jwtOptions.Issuer,
                ValidIssuer = jwtOptions.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
            };

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }

        public override async Task OnConnectedAsync()
        {
            var accessToken = Context.GetHttpContext().Request.Query["access_token"];
            var userPrincipal = ValidateExpiredJWT(accessToken)
                ?? throw new Exception("Invalid token");

            var userId = (userPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value)
                ?? throw new NotFoundException("User not found");

            int count = 0;
            if (clientsNotification.TryGetValue(userId, out count))
                clientsNotification[userId] = count + 1;
            else
                clientsNotification.Add(userId, 1);

            if (clientsNotification[userId] == 1)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, Group.SALES);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var accessToken = Context.GetHttpContext().Request.Query["access_token"];
            var userPrincipal = ValidateExpiredJWT(accessToken)
                ?? throw new Exception("Invalid token");

            var userId = (userPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value)
                ?? throw new NotFoundException("User not found");

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Group.SALES);

            clientsNotification.Remove(userId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
